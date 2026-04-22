using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Gaveta : XRSimpleInteractable    

/*  NÃO CONFUNDIR!
 
   INTERACTABLES (XRSimpleInteractable, XRGrabInteractable, etc) com INTERACTORS (Direct, Near-Far, Teleport, Ray, Poke)
   Gaveta é um Interactable (XRSimpleInteractable). Nosso controle é um interactor (aqui sendo usado com interface IXRSelectInteractor).

   gaveta é um MonoBehaviour (XRSimpleInteractable > XRBaseInteractable > MonoBehaviour).
   "drawer" (filho de "cabinet") é o nome do gameobject, que herda de Object.

   Todos os gameObjects possuem por padrão um componente: o Transform.
   Pode ser referenciado diretamente. Ex.: transform.position = parentTrans.position;

   "O que são componentes? (Onde eles aparecem nos scripts)?". Veja exemplo abaixo: 
   
   public new IXRSelectInteractor interactorObject
         {
             get => (IXRSelectInteractor)base.interactorObject;
             set => base.interactorObject = value;
         }
   
   "interactorObject" é um componente.         */

{
    private float minDistance;
    private float maxDistance;
    private GameObject cabinet;         // pegar posição dele depois
    private Vector3 pos;
    // private bool isTouching;         // em C#, variáveis iniciam com false por default
    private bool isTouching = false;    // ou pode-se fazer a atribuição explícita assim
    
    [SerializeField] private Transform parentTrans;             // isso facilita pegar a posição do pai (apenas arraste a posição dele do Inspector para o campo)
    
    private IXRSelectInteractor interactor;         // interface: ver como funciona nos métodos protected OnSelectEntered e OnSelectExited.
                                                    /* 
                                                        public class XRSimpleInteractable : XRBaseInteractable
                                                          {
                                                          }
                                                     */

                                                    
    
    // fazer o "clamp" da gaveta pra delimitar amplitude de movimento dela.... 
    
    
    
    private void Start()
    {
            
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);               // chama o método do pai 
        interactor = args.interactorObject;       // interactor referencia o interactorObject recebido de args, no momento do contato com o objeto  
        isTouching = true;
        
        /*
        public class SelectEnterEventArgs : BaseInteractionEventArgs
        {
            /// <summary>
            /// The Interactor associated with the interaction event.
            /// </summary>
            
            public new IXRSelectInteractor interactorObject
            {
                get => (IXRSelectInteractor)base.interactorObject;
                set => base.interactorObject = value;                     // Daqui vem a linha "interactor = args.interactorObject;"
            }
        */
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactor = null;                        // interactor referencia null ao sair do contato com o objeto 
        isTouching = false;
    }


    void Update()
    {
        if (!isTouching) return; // "early return"
        
        // transform.position = new Vector3(transform.position.x, transform.position.y, interactor.transform.position.z) ;

        Vector3 dir = interactor.transform.position - transform.position;   // lembrar de vector subtraction, destino - origem

        float dist = Vector3.Dot(dir, transform.forward);           // "O quanto 2 vetores se aproximam" (valor: de -1 a 1)
        
        transform.position += transform.forward * dist;                     // Dist entre vetor do interactor e vetor do GameObject está entre [-1 e 1]
                                                                            // Se dist tem valor 0, os vetores estão na perpendicular...
                                                                            // ... e na perpendicular, transform.position é alterada...
                                                                            // ... e fora da perpendicular, transform.position recebe valor positivo ou negativo no eixo Z (forward).
    }

    private void OnDrawGizmos()   // método tem nativamente um update embutido
    {
        if (!isTouching) return; // "early return" (boa prática de programação)
        
        // transform.position = new Vector3(transform.position.x, transform.position.y, interactor.transform.position.z) ;
        // foi preciso criar o Vector3, pois não é possível fazer algo como:
        // transform.position.z = interactor.transform.position.z;   (isso não funciona no Unity)

        Vector3 dir = interactor.transform.position - transform.position;             // mesma linha lá no Update.  
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, dir * 3);                    // vetor resultante da subtração de destino pela origem
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 3);      // vetor forward (eixo Z) do gameobject
    }
}





/*

// Solução do ChatGPT

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Gaveta : XRSimpleInteractable
{
    [Header("Configuração")]
    [SerializeField] private float maxAbertura = 0.4887f;

    [Tooltip("Eixo de deslizamento no espaço LOCAL do cabinet/pai.")]
    [SerializeField] private Vector3 eixoAberturaLocal = Vector3.left;

    [Tooltip("Opcional. Se vazio, usa o parent.")]
    [SerializeField] private Transform referenciaCabinet;

    private IXRSelectInteractor interactorAtual;
    private Vector3 posFechadaLocal;
    private Vector3 posInteractorInicialCabinetLocal;
    private float aberturaInicial;

    private Transform Cabinet => referenciaCabinet != null ? referenciaCabinet : transform.parent;

    protected override void Awake()
    {
        base.Awake();
        posFechadaLocal = transform.localPosition;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        interactorAtual = args.interactorObject as IXRSelectInteractor;
        if (interactorAtual == null || Cabinet == null)
            return;

        Vector3 eixo = eixoAberturaLocal.normalized;

        posInteractorInicialCabinetLocal =
            Cabinet.InverseTransformPoint(interactorAtual.GetAttachTransform(this).position);

        aberturaInicial = Vector3.Dot(transform.localPosition - posFechadaLocal, eixo);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactorAtual = null;
    }

    private void Update()
    {
        if (interactorAtual == null || Cabinet == null)
            return;

        Vector3 eixo = eixoAberturaLocal.normalized;

        Vector3 posInteractorAtualCabinetLocal =
            Cabinet.InverseTransformPoint(interactorAtual.GetAttachTransform(this).position);

        float deltaNoEixo =
            Vector3.Dot(posInteractorAtualCabinetLocal - posInteractorInicialCabinetLocal, eixo);

        float abertura = Mathf.Clamp(aberturaInicial + deltaNoEixo, 0f, maxAbertura);

        transform.localPosition = posFechadaLocal + eixo * abertura;
    }
}

*/



/*
// Solução do Claude AI:

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Gaveta : XRSimpleInteractable
{
    [Header("Configuração da Gaveta")]
    [SerializeField] private float maxAbertura = 0.4887f;

    // Eixo em que a gaveta se move, no espaço LOCAL do pai (cabinet).
    // Se a gaveta abre em direção ao -Z local do cabinet, use Vector3.back.
    // Ajuste conforme o seu modelo.
    [SerializeField] private Vector3 eixoAberturaLocal = Vector3.left; // (-1, 0, 0)

    private float aberturaNoInicioDoGrab;       // abertura (em metros) quando o grab começou
    private Vector3 posInteractorNoInicioDoGrab; // posição world do interactor quando grab começou
    private IXRSelectInteractor interactorAtual;

    // -------------------------------------------------------------------
    // Callbacks do XR Interaction Toolkit
    // -------------------------------------------------------------------

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        interactorAtual = args.interactorObject;

        // Guarda o "estado zero" no momento do grab
        aberturaNoInicioDoGrab = AberturaAtual();
        posInteractorNoInicioDoGrab = args.interactorObject
                                         .GetAttachTransform(this).position;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactorAtual = null;
    }

    // -------------------------------------------------------------------
    // Atualização frame a frame
    // -------------------------------------------------------------------

    private void Update()
    {
        if (interactorAtual == null) return;

        // Posição atual do interactor no espaço world
        Vector3 posAtual = interactorAtual.GetAttachTransform(this).position;

        // Quanto o interactor se deslocou desde o início do grab (em world space)
        Vector3 deltaWorld = posAtual - posInteractorNoInicioDoGrab;

        // Converte o eixo local da gaveta para world space usando o pai (cabinet).
        // Isso garante que rotações do cabinet sejam levadas em conta.
        Transform referencia = transform;
        Vector3 eixoWorld = referencia.TransformDirection(eixoAberturaLocal).normalized;

        // Projeta o delta do interactor no eixo de movimento da gaveta.
        // Só a componente "ao longo do eixo" importa — movimento lateral é ignorado.
        float deltaNoEixo = Vector3.Dot(deltaWorld, eixoWorld);

        // Nova abertura = abertura no início do grab + deslocamento projetado
        float novaAbertura = Mathf.Clamp(
            aberturaNoInicioDoGrab + deltaNoEixo,
            0f,
            maxAbertura
        );

        AplicarAbertura(novaAbertura);
    }

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    /// <summary>
    /// Retorna a abertura atual em metros,
    /// projetando a localPosition no eixo de movimento.
    /// </summary>
    private float AberturaAtual()
    {
        return Vector3.Dot(transform.localPosition, eixoAberturaLocal);
    }

    /// <summary>
    /// Aplica uma abertura (em metros) à gaveta,
    /// movendo apenas ao longo do eixo definido, sem alterar os outros eixos.
    /// </summary>
    private void AplicarAbertura(float abertura)
    {
        float aberturaAtual = AberturaAtual();
        transform.localPosition += eixoAberturaLocal * (abertura - aberturaAtual);
    }
}
*/







// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.Interactables;
//
// public class Gaveta : XRSimpleInteractable
// {
//     private Gaveta gaveta;
//     private float minDistance = 0f;
//     private float maxDistance = 0.4887f;
//     private bool isSelected = false;
//     [SerializeField] private Vector3 pos;
//     
//     void Start()
//     {
//         gaveta = GetComponent<Gaveta>();
//         
//         // gaveta.GetParent.transform();
//         // minDistance = cabinet.LocalTransform.position.z;                        // z = 0.7546955  (fechado)
//         // maxDistance = minDistance + deltaDistance;                              // z = -0.266     (abertura máx.)
//                                                                                    // deltaDistance = ~0,4887 (amplitude de movimento da gaveta)
//     }
//
//     protected override void OnSelectEntered(SelectEnterEventArgs args)
//     {
//         base.OnSelectEntered(args);
//         isSelected = true;
//     }
//
//     protected override void OnSelectExited(SelectExitEventArgs args)
//     {
//         base.OnSelectExited(args);
//         isSelected = false;
//     }
//     
//     void Update()
//     {
//         if (isSelected)
//         { 
//             Debug.Log("Is Selected");
//             pos = firstInteractorSelecting.GetAttachTransform(this).position;
//             
//         
//          
//         }
//     }
// }
