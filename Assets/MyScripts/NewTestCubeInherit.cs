using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace MyScripts
{
    public class NewTestCubeInherit : XRSimpleInteractable
    {
        // todos os componentes são classes que herdam de Monobehaviour!
        
        private MeshRenderer rend;
        private Color originalColor;
        
        private void Start()
        {
            rend = GetComponent<MeshRenderer>();
            originalColor = rend.material.color;
        }
        
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            
            rend.material.color = originalColor;
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            rend.material.color = Color.green;
        }
    }
}