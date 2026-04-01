using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Oscillator : XRGrabInteractable
{
    // "protected" = visível para esta classe e para filhas (LeverHandle)
    [SerializeField] protected float rotationDuration = 5f;
    [SerializeField] protected float maxAngle = 60f;

    protected float startAngle = -60f;
    protected float timeElapsed = 0f;
    protected Transform cachedTransform;

    public bool activatedInteraction = true;

    // "protected virtual" = filho pode sobrescrever com "override"
    protected virtual void Start()
    {
        cachedTransform = transform; // "transform" é propriedade herdada de Component
    }

    // Não precisa mais de GetComponent nem AddListener!
    // XRBaseInteractable já chama esses métodos quando os eventos disparam.
    // Só precisamos fazer override e chamar base. para não quebrar o toolkit.

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);       // mantém o comportamento interno do XR Toolkit
        activatedInteraction = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        activatedInteraction = true;
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        // comportamento base vazio — LeverHandle vai sobrescrever
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        // idem
    }

    protected virtual void Update()
    {
        if (!activatedInteraction) return;

        if (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f); // smootherstep

            float angle = Mathf.Lerp(startAngle, maxAngle, t);
            cachedTransform.localRotation = Quaternion.Euler(0, 0, angle);

            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0f;
            (startAngle, maxAngle) = (maxAngle, startAngle); // swap elegante
        }
    }
}