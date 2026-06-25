using UnityEngine;
using UnityEngine.InputSystem;

namespace MyScripts
{
    public class CameraToggle : MonoBehaviour
    {
        [SerializeField] private Camera topViewCamera;                  // sua "2ndCamera"
    
        [SerializeField] private InputActionReference toggleAction;     // referencia o Input Action criado para a ativação/desativação da segunda câmera
        
        /* InputActionReference: forma de conseguir obter o input do teclado.
         
         Input Map está no Spaceship Input Action. Dentro dele, existem dois Action Maps: Move2D e Toggle Camera.
         O primeiro contém o controle de movimentação da nave em 2 eixos. O segundo contém o controle de ativação/desativação da câmera
         (pois essa função está relacionada com a visão de cima da nave). Para visualizar ambos, "desdobre o Spaceship Input Action".
         
         Após desdobrar, selecione o Spaceship Action Map/Toggle Camera.
         Arraste ele para o campo "Toggle Action" do script Camera Toggle, que está no objeto CameraManager.         */

        private bool _showingTop = false;                                  // visão de topo inicia como false 

        void Start()
        {
            // Garante estado inicial correto
            topViewCamera.enabled = true;                                  // visão de topo da câmera inicia à mostra 
        }

        private void OnEnable()
        {
            toggleAction.action.performed += ActionOnPerformed;            // quando ativado no ciclo de vida do Unity, método ActionOnPerformed se inscreve em "performed"
        }

        private void ActionOnPerformed(InputAction.CallbackContext ctx)    // lógica de ativação da segunda câmera 
        {
            _showingTop = !_showingTop;                                    // toggle da visão de topo 
            topViewCamera.enabled = _showingTop;                           // faz aparecer segunda câmera (visão de topo)
        }

        private void OnDisable()
        {
            toggleAction.action.performed -= ActionOnPerformed;           // quando desativado no ciclo de vida do Unity, método ActionOnPerformed se desinscreve em "performed"
        }
    }
}