using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// BigButton.cs será apenas um "intermediário" entre a ação física de tocar o botão e a lógica do Counter. 
// O Counter.cs em si é quem será responsável por gerar um evento para o CounterDisplay.cs. 

public class BigButton : XRSimpleInteractable                             // Não usar MonoBehaviour, pois vamos inserir lógica de interação com Grab via código
{
    [SerializeField] private Counter counter;                             // referência ao Counter.cs (precisa ser arrastado via Inspector)
    
    private IXRSelectInteractor interactor;                               // declaração de interactor, que herda de interface

    
    // ============== Don't forget to create a Box Collider around the BigButton object!  ============== //  
    
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)    // "Press G to activate during Play"
    {
        base.OnSelectEntered(args);                                       // seleciona evento da classe mãe
        interactor = args.interactorObject;                               // interactor referencia o objeto detectado nos args da classe mãe
        counter.Increment();                                              // chama o método Increment do Counter.cs
    }
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactor = null;                                                // interactor referencia null
    }

    // Do animation inside Update() later...
    void Update()
    {
        
    }             
}
