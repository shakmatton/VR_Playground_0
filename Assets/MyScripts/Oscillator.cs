using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Oscillator : MonoBehaviour       // Oscillator só possui 1 comportamento ("monobehaviour"): fazer rotação pendular de objetos.
{
    // "protected" = visível para esta classe e para filhas (LeverHandle)
    
    [SerializeField] protected float rotationDuration = 5f;                 // duration of movement
    [SerializeField] protected float maxAngle = 60f;                        // maximum angle
    
    protected float startAngle = -60f;                                      // initial angle
    protected float timeElapsed = 0f;                                       // amount of time passed
    protected Transform savedTransform;                                     // backup of object's Transform component
    
    // "protected virtual" = filho pode sobrescrever com "override"
    protected virtual void Start()
    {
        savedTransform = transform;         // "transform" é propriedade herdada de Component
    }
   
    public void RestartOscillator(float angle)                             // método chamado a partir de "oscillator.RestartOscillator(clampedAngle);", em LeverInteractale.cs      
    {
        float t = Mathf.InverseLerp(startAngle, maxAngle, angle);          // método InverseLerp fará o Lerp com o último ângulo passado pelo clampedAngle (de LeverInteractable.cs)      
        timeElapsed =  t * rotationDuration;                               // equação invertida (ver original em Update) para obter timeElapsed correto  
        // Debug.Log("first angle " + angle);
        enabled = true;                                                    // "enabled = true" reativa o script Oscillator 
    }


    protected virtual void Update()
    {
        if (timeElapsed < rotationDuration)                     // timeElapsed is the amount of small chunks of deltaTime... 
                                                                // ...while lesser than deltaTime, do this below: 
        {
            float t = timeElapsed / rotationDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);                         // smoother step
            float angle = Mathf.Lerp(startAngle, maxAngle, t);                  //  Lerp varia do ângulo inicial ao ângulo máximo, em t (que varia entre [0-1]).
            savedTransform.localRotation = Quaternion.Euler(0, 0, angle);
            // Debug.Log(angle);
            timeElapsed += Time.deltaTime;
        }
        else                                                   // when timeElapsed overflows rotationDuration, do that below:
        {
            timeElapsed = 0f;                                  // resets timeElapsed 
            (startAngle, maxAngle) = (maxAngle, startAngle);   // swap elegante (startAngle from maxAngle, and maxAngle becomes the new startAngle)
        }
    }
}