using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public enum MacanetaAxis        // enumerates all axis
{
    X, Y, Z
}

// **********************************************************************

public class Door : XRSimpleInteractable
{
    [SerializeField] private float maxRotation;

    private MacanetaAxis axis;                                      // axis is now our enumerator
    private Vector3 interactionAxis;                                // interactionAxis guardará um de nossos eixos 
    
    private IXRSelectInteractor interactor;

    private bool isGrabbing = false;

    private void Start()
    {
        switch (axis)                                               // Aqui, a escolha do eixo é feita no início do modo Play 
        {
            case MacanetaAxis.X:                                    // cases usam diretamente o tipo enumerators
                interactionAxis = Vector3.forward;    
                break;
            case MacanetaAxis.Y:
                interactionAxis = Vector3.right;
                break;
            case MacanetaAxis.Z:
                interactionAxis = Vector3.up;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs eventArgs)         // pega a maçaneta da porta
    {
        base.OnSelectEntered(eventArgs);
        interactor = eventArgs.interactorObject;
        isGrabbing = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs eventArgs)           // solta a maçaneta da porta
    {
        base.OnSelectExited(eventArgs);
        interactor = eventArgs.interactorObject;
        isGrabbing = false;
    }

    private void Update()
    {
        if (!isGrabbing) return;
        
        Debug.Log(isGrabbing); 


    }
}