using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyScripts
{
    public class NewSpaceshipController : MonoBehaviour
    {
        
        private Vector2 _moveInput;                                     // representa os eixos X e Y de movimentação da nave
        [SerializeField] private float _speed = 6f;                     // velocidade do spaceShip
        
        [SerializeField] private InputActionReference moveAction;       // referencia o Input Action criado para a movimentação da nave
        
        [SerializeField] private float Xlimit = 10f;                    // limite horizontal de movimentação no eixo X
        [SerializeField] private float Ylimit = 10f;                    // limite vertical de movimentação no eixo Y

        private Vector3 _initialPosition;                               // para registrar a posição inicial da nave
        
        /* InputActionReference: forma de conseguir obter o input do teclado.
         
         Input Map está no Spaceship Input Action. Dentro dele, existem dois Action Maps: Move2D e Toggle Camera.
         O primeiro contém o controle de movimentação da nave em 2 eixos. O segundo contém o controle de ativação/desativação da câmera
         (pois essa função está relacionada com a visão de cima da nave). Para visualizar ambos, "desdobre o Spaceship Input Action".
         
         Após desdobrar, selecione o Spaceship Action Map/Move2D.
         Arraste ele para o campo "Move Action" do script New Spaceship Controller, que está no objeto Spaceship (filho de Spacecraft).         */
        
        private void Start()
        {
            _initialPosition = transform.position;                      // posição inicial da nave salva em variável
        }

        private void OnDrawGizmos()                                     // debug visual (importante para visualizar área e bordas do quadrado) 
        {
            if (!Application.isPlaying)
            {
                _initialPosition = transform.position;
            }
            
            /*  Application.isPlaying é uma propriedade da Unity que retorna true quando o jogo está rodando (Play Mode) e false quando está no Editor parado (Edit Mode).
                Isso importa no nosso caso, pois OnDrawGizmos() é chamado constantemente pelo editor, tanto em Edit Mode quanto em Play Mode.
                
                Sem a condição, ao apertar Play e mover o objeto no jogo, o gizmo seguiria o objeto em tempo real — perdendo a posição inicial.
                Em suma, a ideia é: "memorize a posição atual só enquanto estou editando a cena — depois do Play, não muda mais".             */
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_initialPosition, new Vector3(Xlimit * 2, 0, Ylimit * 2));    // cubo de altura zero desenhado na tela de edição
            
            /* LEMBRETE: na visão Top-Down, Spaceship possui visão dos eixos X e Y. Já Spacecraft (pai de Spaceship) possui eixos X e Z.
               Por isso, vamos tratar nesse script o Y na posição do eixo Z (Xlimit * 2, 0, Ylimit * 2).
               
               Se valor da distância for 10, então haverá uma distribuição de 5 para a esquerda e 5 para a direita.
               Para corrigir isso, Xlimit e Ylimit são multiplicados por 2, dobrando a distribuição (10 para a esquerda, 10 para a direita).
            
               LEMBRETE 2: Da próxima vez, criar lógica no pai e criar filho apenas com "visuals". Aqui, tudo foi feito de modo inverso (visuals no pai e lógica no filho).  */
        }

        private void OnEnable()                                         // ativa objeto na cena
        {
            moveAction.action.performed += OnMove;                      // OnMove ocorrerá quando o evento (botão pressionado) ocorrer
            moveAction.action.canceled  += OnStop;                      // OnStop ocorrerá quando o evento (botão largado) ocorrer
        }

        private void OnMove(InputAction.CallbackContext ctx)            // aqui ocorre a leitura dos valores (botões)
        {
            _moveInput = ctx.ReadValue<Vector2>();                      // salva informações de X e Y do botão, dadas por ctx
        }
        
        private void OnStop(InputAction.CallbackContext obj)
        {
            _moveInput = Vector2.zero;                                 // zera informações de X e Y do botão, dadas por obj
        }

        private void OnDisable()                                       // desativa objeto na cena
        {  
            moveAction.action.performed -= OnMove;                     // OnMove e OnStop são "desligados", para evitar serem chamados inadvertidamente (e para gerenciar memória) 
            moveAction.action.canceled  -= OnStop;
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);             // composição de valores para formar um Vector3
            
            Vector3 predictedPosition = transform.position + (movement * _speed * Time.deltaTime);    // predictedPosition apenas observa posição global da nave e seu movimento
            
            // Os eixos X e Z de predictedPosition recebem valores "clampados", considerando um range de -N a +N.
            predictedPosition.z = Mathf.Clamp(predictedPosition.z, _initialPosition.z - Ylimit, _initialPosition.z + Ylimit);
            predictedPosition.x = Mathf.Clamp(predictedPosition.x, _initialPosition.x - Xlimit, _initialPosition.x + Xlimit);
            
            transform.position = predictedPosition;         // ao final, posição da nave é atualizada com predictedPosition.
        }
    }
}