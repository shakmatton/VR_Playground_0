using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace MyScripts
{
    public class TeleportationActivator : MonoBehaviour
    {
        public XRRayInteractor teleportInteractor;
        public InputActionReference teleportActivatorAction;
        public XRRayInteractor rayInteractor;
        
        void Start()
        {
            teleportInteractor.gameObject.SetActive(false);
            teleportActivatorAction.action.performed += Action_performed;
            rayInteractor.uiHoverEntered.AddListener(x => DisableTeleportRay());
        }

        private void Action_performed(InputAction.CallbackContext obj)           // evento chamado no Start()
        {
            teleportInteractor.gameObject.SetActive(true);                       // Inicia teleportInteractor com "true"
        }

        public void DisableTeleportRay()
        {
            teleportInteractor.gameObject.SetActive(false);
        }

        void Update()
        {
            if (teleportActivatorAction.action.WasReleasedThisFrame())          // se botão de teleport foi liberado...      
            {
                teleportInteractor.gameObject.SetActive(false);                 // ... teleportInteractor muda para "falso"
            }
        }
    }
}
