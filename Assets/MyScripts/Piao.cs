using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Piao : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
    private PiaoNumbers piaoNumbers; // <-- referência à instância

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;

        // Busca o componente PiaoNumbers no filho
        piaoNumbers = GetComponentInChildren<PiaoNumbers>();

        if (piaoNumbers == null)
            Debug.LogWarning("PiaoNumbers não encontrado nos filhos do Piao!");     
        // Debug.Log: mostra mensagens informativas em cinza no Console.
        // Debug.LogWarning: destaca avisos em amarelo para possíveis problemas.
        // Debug.LogError: exibe erros em vermelho, indicando falhas críticas que precisam ser corrigidas.
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (audioSource && audioClip)
            audioSource.Play();

        if (piaoNumbers != null)
            piaoNumbers.StartSpinning(); // <-- instância, não classe
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (audioSource && audioSource.isPlaying)
            audioSource.Stop();

        if (piaoNumbers != null)
            piaoNumbers.StopSpinning(); // <-- instância, não classe
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}


// using System.Collections;
// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.Interactables;
//
// public class Piao : MonoBehaviour
// {
//     private XRGrabInteractable grabInteractable;
//     
//     private AudioSource audioSource;
//     [SerializeField] private AudioClip audioClip;
//     
//     // [SerializeField] private float piaoSpeed;
//     // private float rotationY;
//     
//     void Start()
//     {
//         grabInteractable = GetComponent<XRGrabInteractable>();
//         grabInteractable.selectEntered.AddListener(OnGrabbed);
//         grabInteractable.selectExited.AddListener(OnReleased);
//         
//         // piaoSpeed = 0.0f;
//
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//         {
//             audioSource = gameObject.AddComponent<AudioSource>();
//         }
//
//         audioSource.clip = audioClip;
//         
//     }
//
//     void OnGrabbed(SelectEnterEventArgs args)
//     {
//         // rotationY = gameObject.transform.rotation.y;
//         // piaoSpeed = 1.0f;
//         // rotationY = piaoSpeed * Time.deltaTime;
//
//         if (audioSource && audioClip)
//         {
//             audioSource.Play();
//         }
//        
//         PiaoNumbers.StartSpinning();
//     }
//     void OnReleased(SelectExitEventArgs args)
//     {
//         // rotationY = 0.0f;
//
//         if (audioSource && audioSource.isPlaying)
//         {
//             audioSource.Stop();
//         }
//         
//         PiaoNumbers.StopSpinning();
//     }
//
//     void OnDestroy()
//     {
//         grabInteractable.selectEntered.RemoveListener(OnGrabbed);
//         grabInteractable.selectExited.RemoveListener(OnReleased);
//     }
//
// }
