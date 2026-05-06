using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


// VER INSIGHT SOBRE COLLIDERS, CALLBACKS E FUNÇÕES LAMBDA NOS COMENTÁRIOS (ao final do script)
// (minha versão primeiro, depois a versão do ClaudeAI) 


public class Door : XRSimpleInteractable               // a porta é um interactable 
{
    // private List<int> _list = new List<int>();      // exemplo de como criar uma lista de inteiros
    // private float currentRotation;
    // private float aux;
    // private bool hasTouched = false;                // verifica se está ou não interagindo com algo
    // private bool toBePaused = true;                 // flag de verificação de animação parada ou contínua
    // [SerializeField] private float doorSpeed;          // velocidade da porta
    // [SerializeField] private float maxRotation;        // ângulo máximo que a porta pode girar
    
    
    
    
    private IXRSelectInteractor interactor;                     // interactor é uma implementação da interface IXRSelectInteractor, que herda da interface IXRInteractor.
    
    [SerializeField] private float openAngle = -90f;            // ângulo da porta quando está aberta
    [SerializeField] private float animationDuration = 0.8f;    // a duração da animação

    private float fromAngle = 0f;   // ângulo de partida
    private float toAngle = 0f;     // ângulo de destino

    private float timeElapsed = 0f;
    
    private bool isAnimating = false;   // flag de animação 
    private bool isOpen = false;        // flag de status (porta aberta/fechada)
   
    
    private void Start()
    {
        // rotações são negativas e ocorrem no eixo z (porta gira "se afastando") 
        // maxRotation = -90f;
        // doorSpeed = -50f;

        // currentRotation = 0f;
        // aux = currentRotation;
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        isOpen = !isOpen;                               // alterna status da porta
        
        fromAngle = transform.localEulerAngles.z;       // inicia do ângulo atual (considera também o toque no meio da animação)
        
        
        /* Diferença entre usos de EULER e QUATERNION: "Euler é para humanos, Quaternion é para a matemática".

           LEITURA: Unity te entrega o ângulo Z como float legível (Euler)
           fromAngle = transform.localEulerAngles.z;   // ex: 270f

           ESCRITA: Unity exige um Quaternion para setar rotação
           Quaternion.Euler() converte seus ângulos legíveis de volta para Quaternion
           transform.localRotation = Quaternion.Euler(0f, 0f, angle);
           
           Quaternion.Euler(x, y, z), usado mais abaixo no código, é só um conversor. 
           Você passa os ângulos que calculou, e ele devolve o Quaternion equivalente que o Unity entende.
           
           Nunca manipule os valores x y z w de um Quaternion diretamente — eles não têm significado intuitivo. 
           Sempre use Quaternion.Euler() ou métodos como Quaternion.Slerp().            */
        
        
        // localEulerAngles sempre retorna valores entre 0° e 360°. Nunca negativos.
        // Então, quando a porta está em -90°, o Unity diz que ela está em 270°. São o mesmo ângulo físico, mas representados diferente.
        
        if (fromAngle > 180f)                           // corrige ângulos acima de 180 que o Unity retorna (ex: 270 em vez de -90)
        {
            fromAngle -= 360f;                          // ângulo passa a ter sinal invertido (o que evita o movimento da porta pelo sentido inverso) 
        } 
        
        /* Explicação do trecho acima:
        
        O que aconteceria com o Lerp sem a correção é:
        
        fromAngle = 270f   (porta aberta, que fisicamente é -90°)
        toAngle   = 0f     (porta fechada)
         
        Lerp(270, 0, t) → animaria: 270 → 240 → 180 → 90 → 0
        
        Logo, a porta giraria 270 graus no sentido errado, dando quase uma volta completa, em vez de fechar pelos 90° corretos.
        
        O trecho descomentado acima resolve o problema:
        
        fromAngle = -90f   (agora representado corretamente)
        toAngle   = 0f
         
        Lerp(-90, 0, t) → animaria: -90 → -60 → -30 → 0  ✓
        
        */

        toAngle = isOpen ? openAngle : 0f;              // ângulo de destino toAngle será -90 (openAngle) ou 0;
        timeElapsed = 0f;                               // reset do timeElapsed
        isAnimating = true;                             // flag de animação ativado
    }

    private void Update()
    {
        if (!isAnimating) return;                       // não faz nada se não houver animação

        if (timeElapsed < animationDuration)            // se timeElapsed acumulado ainda é menos que a duração da animação... 
        {                                               // ... usar LERP.
            
            float t = timeElapsed / animationDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);               // smootherstep (igual ao Oscillator)
            
            float angle = Mathf.Lerp(fromAngle, toAngle, t);               // angle recebe interpolação entre ângulos de origem e destino
            
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);     // angle é usado para calcular rotação da porta
                                                                           // em seguida, transform da porta recebe essa rotação calculada 
            
            timeElapsed += Time.deltaTime;                                 // timeElapsed é acumulado 
        }
        else
        {
            // se timeElapsed acumulado ultrapassar a duração da animação...
            transform.localRotation = Quaternion.Euler(0f, 0f, toAngle);  // garante que a porta trava exatamente no ângulo destino.  
                                                                           
            isAnimating = false;
        }
    }
}


// ============================================================================================================================================
// ============================================================================================================================================
    
    
    // protected override void OnSelectEntered(SelectEnterEventArgs args)         // porta tocada
    // {
    //     base.OnSelectEntered(args);                                            // método herdado de XRBaseInteractable
    //     interactor = args.interactorObject;                                    // interactor recebe o objeto "controle" que toca o interactable
    //     
    //     /* The line above extracts the Interactor (the object initiating an action, like a VR controller or ray) from the event data passed during an interaction.                     
    //        It identifies which specific interactor triggered an event—such as a "Select," "Hover," or "Activate" event—on an interactable object.           
    //        The interactorObject property is part of BaseInteractionEventArgs class, which is the base for more specific event types like SelectEnterEventArgs or HoverEnterEventArgs.
    //            
    //        Example: when a user grabs a virtual cube, the cube's OnSelectEntered event is fired. By using this line, a developer can determine
    //        which hand (left or right) is holding the cube to trigger hand-specific logic, such as haptic feedback or changing a material.
    //                   
    //        The interactorObject usually returns an interface like IXRInteractor rather than a direct GameObject,
    //        though the underlying GameObject can be accessed via interactorObject.transform.gameObject.                          */
    //     toBePaused = !toBePaused;
    // }
    //
    // /*protected override void OnSelectExited(SelectExitEventArgs args)           // porta largada
    // {
    //     base.OnSelectExited(args);
    //     interactor = null;                          // interactor é nulo no ato do "toque desativado"
    //     hasTouched = false;
    // }*/
    //
    // private void Update()
    // {
    //     //if (!hasTouched) return;    // Update só executará quando houver o toque
    //     
    //     /* ideia: no futuro, o transform da porta deve acompanhar o transform do controle. Será preciso fazer a porta se mover via rotação em um eixo somente.
    //        No momento, fazer apenas a porta girar ao tocar ela. */
    //     
    //     // Usar Lerp? Herdar Oscillator? Clamp?
    //     if (!toBePaused)
    //     {
    //         transform.Rotate(0f, 0f, doorSpeed * Time.deltaTime, Space.Self);    // gira a porta no eixo Z
    //     }
    //     
    //
    //     /*
    //     currentRotation = maxRotation;
    //     
    //     
    //     if (currentRotation > maxRotation)
    //     {
    //         doorSpeed = !doorSpeed;
    //         maxRotation = aux;
    //         aux = currentRotation;
    //     }
    //     
    //     transform.Rotate(0f, 0f, doorSpeed * Time.deltaTime, Space.Self);    // gira a porta no eixo Z
    //     
    //     
    //     /*
    //     Quaternion rotation = transform.localRotation;   // variável armazena posição local da porta
    //     rotation.z += doorSpeed * Time.deltaTime;         // gira a porta e atribui valor float da porta girada de volta à variável 
    //     transform.localRotation = rotation;              // porta recebe rotação atualizada da variável
    //     */
    //     
    // }
// }








/*
 
                                                    ============= MINHA VERSÃO ===============
                            
                            
   /*  Insights sobre Callbacks e Funções Lambda.
       Explicação do contexto: objeto "Porta" não funcionava. O passo de Debug não mostrava nada no Console.        
       Processo de pensamento: verificar a fundo como Colliders funcionam no Unity. 
       A investigação levou ao XRBaseInteractable.cs, que tinha no código:
       
           public List<Collider> colliders => m_Colliders;   // lista de colliders definida (observe o uso de função lambda aqui)
   
       E, no método Awake do mesmo código:
       
           protected virtual void Awake()
           {
              // If no colliders were set, populate with children colliders
              if (m_Colliders.Count == 0)
              {
                  GetComponentsInChildren(m_Colliders);
          
                  // Skip any that are trigger colliders since these are usually associated with snap volumes.
                  // If a user wants to use a trigger collider, they must serialize the reference manually.
          
                  m_Colliders.RemoveAll(col => col.isTrigger);
              }
           ...
       
       A linha "m_Colliders.RemoveAll(col => col.isTrigger);" estimulou a discussão sobre como funcionam Callbacks.
       No código da porta (Door.cs), criou-se um exemplo para ilustrar a ideia:
       
       Digamos que queiramos criar uma função dentro de Start():     
           
           RemoveAll(teste);       // veja abaixo como "teste" é usado 
    
       E no corpo do código de Door.cs, criamos um teste:
               
           private bool teste(int a)      // aqui, teste é apenas uma função comum, que retorna true. 
           {
               return true;
           }

           public delegate bool Callback(int i);       // delegate é um método especial que define que a variável "i" vai conter a função "teste".
           
           public void RemoveAll(Callback callback)    // função RemoveAll definida mais acima recebe um callback, ou seja, o "teste" (que é uma outra função) como parâmetro.
           {
               foreach (int i in _list)                // nesse exemplo, o loop procura se o elemento int i está em um conjunto (uma lista hipotética)
               {
                   bool result = callback(i);          // salva o resultado do boolean retornado por callback(i)
                   if (result == true)                 // se resultado for verdadeiro...
                   {
                       _list.Remove(i);                // remove o int i da lista.
                   }
               }
           }
           
       Voltando ao contexto dos colliders...
       Toda a explicação acima foi para mostrar que aquilo tudo poderia ser equivalente ao conteúdo dessa função lambda: 
       "m_Colliders.RemoveAll(col => col.isTrigger);"
       
       OBS.: sobre colliders, o Unity irá considerar OnCollision em vez de OnTrigger como forma de usar o campo "isTrigger" no Inspector do objeto na cena.
       É possível considerar ambas as formas, mas a caixinha do isTrigger deverá estar marcada para a detecção do collider ativar o evento de trigger.
   
   * /
 
 
 
                                                    ============= VERSÃO CLAUDE AI ===============
 
    Insights sobre Callbacks e Funções Lambda.
    Explicação do contexto: objeto "Porta" não funcionava. O passo de Debug não mostrava nada no Console.        
    Processo de pensamento: verificar a fundo como Colliders funcionam no Unity. 
    A investigação levou ao XRBaseInteractable.cs, que continha no método Awake:
        
        protected virtual void Awake()
        {
            if (m_Colliders.Count == 0)
            {
                GetComponentsInChildren(m_Colliders);
                m_Colliders.RemoveAll(col => col.isTrigger);
            }
        ...
    
    A linha "m_Colliders.RemoveAll(col => col.isTrigger);" estimulou a discussão sobre Callbacks e Lambdas.
    
    
    --- O QUE É UM CALLBACK? ---
    
    Um callback é uma função passada como argumento para outra função.
    Em vez de passar um valor (int, string...), você passa um comportamento — 
    um bloco de código que será executado dentro da outra função.

    Para ilustrar, criou-se um exemplo dentro de Door.cs.
    No Start(), a chamada seria:
    
        RemoveAll(teste);    // "teste" é o callback — passado sem parênteses
    
    No corpo da classe, definimos as três peças necessárias:
    
        // 1. A função que será passada como callback.
        //    Recebe um int e retorna bool — critério de remoção.
        private bool teste(int a)
        {
            return true;
        }

        // 2. O delegate: não é um método, é uma DEFINIÇÃO DE TIPO.
        //    Ele declara o "contrato de assinatura" que qualquer callback
        //    passado para RemoveAll deve respeitar:
        //    precisa receber um int e retornar um bool.
        //    "teste" respeita esse contrato, então pode ser passado.
        public delegate bool Callback(int i);
        
        // 3. RemoveAll recebe o callback como parâmetro.
        //    Itera a lista e remove todo elemento para o qual
        //    o callback retornar true.
        public void RemoveAll(Callback callback)
        {
            foreach (int i in _list)
            {
                bool result = callback(i);   // executa a função passada, usando i como argumento
                if (result == true)
                {
                    _list.Remove(i);
                }
            }
        }
    
    
    --- O QUE É UMA FUNÇÃO LAMBDA? ---
    
    Uma lambda é um callback escrito inline, sem precisar declarar uma função separada com nome.
    A sintaxe é:    parâmetro => expressão
    Significa: "receba este parâmetro e retorne o resultado desta expressão."
    
    Comparando os dois equivalentes:
    
        RemoveAll(teste);          // callback nomeado
        RemoveAll(a => true);      // lambda equivalente (retorna true para qualquer elemento)
    
    
    --- FECHANDO O CICLO: A LINHA DO UNITY ---
    
    Agora fica legível:
    
        m_Colliders.RemoveAll(col => col.isTrigger);
    
    Tradução: "Remova da lista todo 'col' (collider) para o qual col.isTrigger for true."
    É exatamente o RemoveAll(teste) do exemplo, com duas diferenças:
        - o callback não tem nome (foi escrito diretamente como lambda)
        - o critério de remoção não é "return true" fixo, mas sim a propriedade isTrigger de cada collider
    
    
    --- OBSERVAÇÃO: O SÍMBOLO => PODE TER DOIS SIGNIFICADOS DISTINTOS ---
    
    Também foi encontrada no XRBaseInteractable.cs esta linha:
    
        public List<Collider> colliders => m_Colliders;
    
    Apesar de usar o mesmo símbolo =>, isso NÃO é uma lambda passada como callback.
    É uma sintaxe curta de propriedade, equivalente a:
    
        public List<Collider> colliders { get { return m_Colliders; } }
    
    O => aqui apenas abrevia o getter. São usos diferentes do mesmo símbolo.
    
    
    --- OBSERVAÇÃO FINAL: COLLIDERS E TRIGGER ---
    
    O Unity considera OnCollision e OnTrigger como duas formas distintas de detecção.
    A caixinha "isTrigger" no Inspector do collider determina qual caminho será usado.
    Se marcada, o evento de Trigger é ativado. Se desmarcada, o evento de Collision é ativado.
    Para usar trigger via código sem depender do Inspector, o collider deve ser configurado
    com isTrigger = true diretamente no script.
*/
 
 
