using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// BigButton.cs será apenas um "intermediário" entre a ação física de tocar o botão e a lógica do Counter. 
// O Counter.cs em si é quem será responsável por gerar um evento para o CounterDisplay.cs. 

public class BigButton : XRSimpleInteractable                             // Não usar MonoBehaviour, pois vamos inserir lógica de interação com Grab via código
{
    [SerializeField] private CycleAnimation cycleAnimation;               // faz referência ao script de animação CycleAnimation
    
    [SerializeField] private Counter counter;                             // referência ao Counter.cs (precisa ser arrastado via Inspector)
                                                                          // [SerializeField] é a conexão entre o BigButton e o Counter
    
    private IXRSelectInteractor interactor;                               // declaração de interactor, que herda de interface

    private Vector3 startPosition;                                        // posição de início do botão
    private float finalPositionZ = -0.0222f;
    
    // ==============  DO NOT FORGET TO CREATE A BOX COLLIDER AROUND THE BIGBUTTON OBJECT!  ============== //  

    private void Start()
    {
        startPosition = transform.localPosition;                         // registra posição inicial do botão na variável startPosition
    } 
    
    public event Action<Vector3, float> OnButtonPushAnimation;           // criando evento delegate para animação (apenas para treinamento)
    
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)    // "Press G to activate during Play"
    {
        base.OnSelectEntered(args);                                       // seleciona evento da classe mãe
        interactor = args.interactorObject;                               // interactor referencia o objeto detectado nos args da classe mãe
        counter.Increment();                                              // chama o método Increment lá do Counter.cs (o qual, por sua vez, irá disparar um evento)
        
        OnButtonPushAnimation?.Invoke(startPosition, finalPositionZ);     // disparo do evento delegate (deve ser ouvido e inscrito por método de CycleAnimation)
    }
    
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactor = null;                                                // interactor referencia null
    }
}
