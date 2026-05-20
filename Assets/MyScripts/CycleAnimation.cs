using UnityEngine;

public class CycleAnimation : MonoBehaviour
{
    // private Vector3 savedTransform;                  // guarda posição original do objeto (no caso do BigButton, será o startPosition, passado por parâmetro)
    private float timeElapsed;                          // tempo decorrido dentro do Lerp
    private float terminalPositionDown = -0.0222f;
    
    [SerializeField] BigButton bigButton;
    
    void Start()
    {
        bigButton.OnButtonPushAnimation += HandleAnimation;
    }

    private void HandleAnimation(Vector3 startPosition)
    {
        bigButton.savedTransform = startPosition;                 // savedTransform atua como backup de startPosition
        Debug.Log("Testando valor inicial local na origem:" + bigButton.savedTransform);
    }

    void Update()
    {
        if (timeElapsed > terminalPositionDown)
        {
            
        }
        else
        {
            
        }
    }
}
