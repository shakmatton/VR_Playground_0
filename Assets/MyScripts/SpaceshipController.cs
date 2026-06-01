using UnityEngine;
using UnityEngine.InputSystem;

namespace MyScripts
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private float _speed = 6f;                     // velocidade do spaceShip
        private Vector2 _moveInput;                                     // _moveInput guarda o valor do Vector2 lido em OnMove
        
        private PlayerInput _playerInput;                               // componente inserido no objeto Spaceship
                                                                        // esse componente tem o InputAction criado para o controle do Spaceship
                                                                        
        /*  No componente Player Input, configurar o campo Behavior com "Invoke C Sharp Events"
            Em Edit > Project Settings > Input Actions, criar uma Action Maps chamado "Spaceship".
            
            Ali, criar um "Move2D" com Action Type = Value, e Control Type = Vector2. 
            Ao lado do Move2D, clique no + e selecione "Add Up/Down/Left/Right composite".
           
            Insira ele como filho do Move2D. Esse filho deve ser um 2D Vector.
            Ali, insira os botões desejados (ex.: 8456, em vez de WASD), e marque a caixinha "Keyboard & Mouse".         */  


        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();                     // pega o PlayerInput do mesmo GameObject
        }

        private void OnEnable()
        {
            // "Move2D" é o nome exato da action no Input Actions asset
            _playerInput.actions["Move2D"].performed += OnMove;             // "Hardbinded" por conta da natureza do "Invoke C Sharp Events" 
            _playerInput.actions["Move2D"].canceled  += OnMove;
        }

        private void OnDisable()
        {
            _playerInput.actions["Move2D"].performed -= OnMove;
            _playerInput.actions["Move2D"].canceled  -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext ctx)              // aqui ocorre a leitura dos valores (botões)  
        {
            if (ctx.performed)                                            // se botão pressionado...
                _moveInput = ctx.ReadValue<Vector2>();                    
            else if (ctx.canceled)                                        // se botão largado...  
                _moveInput = Vector2.zero;
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);             // composição de valores para formar um Vector3
            transform.localPosition += movement * _speed * Time.deltaTime;         // Vector3 formado atualiza a posição do spaceship
        }
    }
}