using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lamp : MonoBehaviour
{
    private Light spotLight;
    private XRGrabInteractable grabInteractable;
    
    [SerializeField] 
    private bool myLightBool = false;                           // Variável privada (inicia como "false")
    
    void Start()                                                // Configura Light e Grab logo de início
    {
        spotLight = GetComponentInChildren<Light>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.activated.AddListener(OnActivated);    // Inscreve evento "OnActivated" do grabInteractable, adicionando ele ao listener
        UpdateLight();                                          // Chama método que recebe valor corrente de myLightBool
    }
    private void UpdateLight()                                  // Método que recebe valor corrente de myLightBool
    {
        if (spotLight != null)                               // Se houver um spotLight válido...
            spotLight.enabled = myLightBool;                    // ...objeto luminaria acende/apaga, baseado no valor corrente de myLightBool
    }

    private bool LightStatus                                    // Propriedade pública que controla a luz (retorna bool)
    {
        get => myLightBool; 
        set 
        { 
            myLightBool = value;
            UpdateLight();                                      // Chama método que faz objeto acender/apagar luz
        }
    }
    
    private void OnActivated(ActivateEventArgs args)            // Método que manipula a Propriedade pública LightStatus (quando o Trigger é ativado)
    {
        LightStatus = !LightStatus;                             // Propriedade LightStatus recebe internamente o valor corrente de myLightBool
    }
    
    void OnValidate()                                           // Para funcionar no Inspector durante o EDIT MODE
    {
        spotLight = GetComponentInChildren<Light>();            // spotLight referencia a luz...
        
        if (Application.isPlaying)                              // Se estiver em "modo Play"...
        {
            LightStatus = myLightBool;                          // ...LightStatus recebe valor diretamente da variável
        }
        else
        {
            UpdateLight();                                      // Senão, chama método que faz objeto acender/apagar luz
        }
    }
    
    void OnDestroy()                                            // método que remove o evento "OnActivated" do grabInteractable ao final do "modo Play"
    {
        grabInteractable.activated.RemoveListener(OnActivated); // desinscreve o evento
    }
}



// public class Lamp2 : MonoBehaviour
// {
//     private Light spotLight;  
//     private bool lightStatus = false;
//     private XRGrabInteractable grabInteractable;
//     
//     void Start()
//     {
//         spotLight = GetComponentInChildren<Light>();
//         spotLight.enabled = false;
//         
//         grabInteractable = GetComponent<XRGrabInteractable>();
//         grabInteractable.activated.AddListener(OnActivated);
//     }
//     
//     private void OnActivated(ActivateEventArgs args)
//     {
//         // Alterna o estado apenas uma vez por ativação
//         lightStatus = !lightStatus;
//         spotLight.enabled = lightStatus;
//     }
//     
//     void OnDestroy()
//     {
//         grabInteractable.activated.RemoveListener(OnActivated);
//     }
// }