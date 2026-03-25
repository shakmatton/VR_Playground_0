using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverHandle : MonoBehaviour            // dica: procurar fazer nome dos componentes ser mais descritível e reutilizável
{                                                   // exemplo: "Rotator" (descreve o que componente faz, e serve para vários casos que precisem de rotação)
    
    // private XRGrabInteractable _grabInteractable;
    [SerializeField] private float rotationDuration = 1f;     // Precisamos disso para trabalhar o LERP (Linear Interpolation)
    [SerializeField] private float _maxAngle = 60f;           // Ver link: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/ 
    
    private float _startAngle = -60f;                         // ângulo inicial
    private float _timeElapsed = 0f;                          // acumulador de "fatias" de tempos decorridos
    
    private Transform _transform;
    
    void Start()
    {
        // _grabInteractable = GetComponent<XRGrabInteractable>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (_timeElapsed < rotationDuration)                                    // Soma de tempos decorridos vai de 0 a 1. Então, executa-se o if enquanto timeElapsed < 1. 
        {
            float t = _timeElapsed / rotationDuration;                          // Cálculo do passo ("velocidade") de rotação. 
            t = t * t * (3f - 2f * t);                                          // linha de código especial para aplicar smooth in e smooth out
            
            float angle = Mathf.Lerp(_startAngle, _maxAngle, t);                // Ângulo será o resultado de Lerp, entre starAngle e maxAngle, usando t passos.
            _transform.localRotation = Quaternion.Euler(0, 0, angle);           // OBS.: Diferença entre Rotation ("Vá para ângulo X") e Rotate ("Some X ângulos ao ângulo Y atual").
            
            Debug.Log("Angle: " + angle + " t: " + t);
            _timeElapsed += Time.deltaTime;                                     // timeElapsed acumula os deltaTimes...
        }
        else                                                                    // Soma de tempos decorridos vai de 1 a 0. Então, executa-se o else enquanto timeElapsed >= 0.
        {
            _timeElapsed = 0f;                                                  // timeElapsed é resetado.
            
            float aux = _startAngle;                                            // valores de startAngle e maxAngle são trocados para permitir a animação reversa.
            _startAngle = _maxAngle;
            _maxAngle = aux;
        }
    }
}

// usar Simple Ray Interactor depois...