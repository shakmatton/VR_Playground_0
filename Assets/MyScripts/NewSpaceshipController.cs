using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


// tentar refazer sozinho depois, para os asteroides...

namespace MyScripts
{
    public class NewSpaceshipController : MonoBehaviour
    {
        private Vector2 _moveInput;                                     // representa os eixos X e Y de movimentação da nave
        [SerializeField] private float _speed = 6f;                     // velocidade do spaceShip
        
        [SerializeField] private InputActionReference moveAction;       // referencia o Input Action criado para a movimentação da nave
        
        /* InputActionReference: forma de conseguir obter o input do teclado.
         
         Input Map está no Spaceship Input Action. Dentro dele, existem dois Action Maps: Move2D e Toggle Camera.
         O primeiro contém o controle de movimentação da nave em 2 eixos. O segundo contém o controle de ativação/desativação da câmera
         (pois essa função está relacionada com a visão de cima da nave). Para visualizar ambos, "desdobre o Spaceship Input Action".
         
         Após desdobrar, selecione o Spaceship Action Map/Move2D.
         Arraste ele para o campo "Move Action" do script New Spaceship Controller, que está no objeto Spaceship (filho de Spacecraft).         */ 
        
        private void OnEnable()                                         // ativa objeto na cena
        {
            moveAction.action.performed += OnMove;                      // OnMove ocorrerá quando o evento (botão pressionado) ocorrer
            moveAction.action.canceled  += OnStop;                      // OnStop ocorrerá quando o evento (botão largado) ocorrer
            
            SpaceshipLimits.OnSpaceshipOutOfBounds += HandleOutOfBounds;
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
            SpaceshipLimits.OnSpaceshipOutOfBounds -= HandleOutOfBounds;
        }
        
        private void HandleOutOfBounds(Vector3 clampedPosition)
        {
            transform.position = clampedPosition;
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);             // composição de valores para formar um Vector3
            transform.localPosition += movement * _speed * Time.deltaTime;              // Vector3 formado atualiza a posição do spaceship
        }
    }
}