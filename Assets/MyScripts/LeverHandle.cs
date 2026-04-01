using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverHandle : Oscillator
{
    // Nenhum GetComponent, nenhum AddListener aqui.
    // Tudo já está em Oscillator. LeverHandle só especializa o comportamento.

    protected override void Start()
    {
        base.Start(); // inicializa cachedTransform e tudo do pai
        // inicializações específicas do LeverHandle podem entrar aqui
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        activatedInteraction = false; // Trigger congela a animação
        Debug.Log("Trigger pressionado — alavanca congelada.");
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        activatedInteraction = true;  // soltar Trigger retoma a animação
        Debug.Log("Trigger solto — animação retomada.");
    }

    // Update não precisa ser sobrescrito agora:
    // o Update de Oscillator já roda automaticamente por herança.
    // Se LeverHandle precisar de comportamento extra no futuro, basta:
    //
    // protected override void Update()
    // {
    //     base.Update(); // roda a oscilação do pai
    //     // lógica extra aqui
    // }
}