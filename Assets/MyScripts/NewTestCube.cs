using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace MyScripts
{
    public class NewTestCube : MonoBehaviour
    {
        private MeshRenderer rend;
        private Color originalColor;
        private XRSimpleInteractable interactable;
        
        private void Start()
        {
            rend = GetComponent<MeshRenderer>();
            interactable = GetComponent<XRSimpleInteractable>();
            originalColor = rend.material.color;
            
            interactable.hoverEntered.AddListener(OnHoverSelected);   // fazer removelistener depois
            interactable.hoverExited.AddListener(OnHoverExited);
        }
        // all components é uma classe (que herda de monobehaviour)!!!

        private void OnHoverSelected(HoverEnterEventArgs args)
        {
            rend.material.color = Color.red;
        }  
        
        private void OnHoverExited(HoverExitEventArgs args)
        {
            rend.material.color = originalColor;
        }
        
    }
}