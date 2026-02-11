using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Lamp1 : MonoBehaviour
{
    private Light spotLight;  
    public bool LightStatus = false;
    
    void Start()
    {
        spotLight = GetComponentInChildren<Light>();    // spotLight pega referência para filho do tipo Light
        spotLight.enabled = false;                      // spotLight começa na cena com luz apagada
    }
    
    // criar método para acender a lampada como 2nd action (no trigger, após o grab)...

    public void Trigger_LightOn()
    {
        // LightStatus = !LightStatus;
        // spotLight.enabled = LightStatus;
        spotLight.enabled = true;
    }
    
    public void Trigger_LightOff()
    {
        // LightStatus = !LightStatus;
        // spotLight.enabled = LightStatus;
        spotLight.enabled = false;
    }
    
    /*    
    void Switch()
    {
        LightStatus = !LightStatus;
        GetComponentInChildren<Light>().enabled = LightStatus;
    }
 
    public void Grab_LightOn()
    {
        spotLight.enabled = true;
    }

    public void Grab_LightOff()
    {
        spotLight.enabled = false;
    }
    */
    
}

