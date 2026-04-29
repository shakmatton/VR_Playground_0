using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// Objetivo: treinar mais o XRInteraction Toolkit!
// Focar nas interações.

public class Door : XRSimpleInteractable            // a porta é um interactable 
{
    [SerializeField] private float maxRotation;     // ângulo máximo que a porta pode girar
    private float doorSpeed = 10f;
    
    
    private IXRSelectInteractor interactor;         // interactor é uma implementação da interface IXRSelectInteractor,
                                                    // que herda da interface IXRInteractor.

    private bool isTouching = false;                // verifica se está ou não interagindo com algo

    private void Start()
    {
        maxRotation = 90f;
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs eventArgs)         // porta tocada
    {
        base.OnSelectEntered(eventArgs);                                            // método herdado de XRBaseInteractable
        interactor = eventArgs.interactorObject;                                    // interactor recebe o objeto "controle" que toca o interactable
        
        /* The line above extracts the Interactor (the object initiating an action, like a VR controller or ray)
           from the event data passed during an interaction. 
                      
           It identifies which specific interactor triggered an event—such as a "Select," "Hover," or "Activate" event—on an interactable object.
           Property Details: The interactorObject property is part of the BaseInteractionEventArgs class, 
           which is the base for more specific event types like SelectEnterEventArgs or HoverEnterEventArgs.
               
           Example: when a user grabs a virtual cube, the cube's OnSelectEntered event is fired. 
           By using this line, a developer can determine which hand (left or right) is holding the cube
           to trigger hand-specific logic, such as haptic feedback or changing a material.
                      
           The interactorObject usually returns an interface like IXRInteractor rather than a direct GameObject,
           though the underlying GameObject can be accessed via interactorObject.transform.gameObject.                          */
        
        isTouching = true;  // toque confirmado
    }

    protected override void OnSelectExited(SelectExitEventArgs eventArgs)           // porta largada
    {
        base.OnSelectExited(eventArgs);
        interactor = eventArgs.interactorObject;
        isTouching = false;
    }

    private void Update()
    {
        if (!isTouching) return;    // Update só executará quando houver o toque
        
        // ideia: no futuro, o transform da porta deve acompanhar o transform do controle. Será preciso fazer a porta se mover via rotação em um eixo somente.
        // no momento, fazer apenas a porta girar ao tocar ela.

        transform.Rotate(0f, 0f, doorSpeed * Time.deltaTime, Space.Self);    // gira a porta no eixo Z
        
        
        // não funciona ainda...
        
        
        
        /*
        Quaternion rotation = transform.localRotation;   // variável armazena posição local da porta
        rotation.z += doorSpeed * Time.deltaTime;         // gira a porta e atribui valor float da porta girada de volta à variável 
        transform.localRotation = rotation;              // porta recebe rotação atualizada da variável
        */
        
        Debug.Log(isTouching); 


    }
}