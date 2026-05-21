using UnityEngine;

public class CycleAnimation : MonoBehaviour
{
    [SerializeField] private BigButton bigButton;       // referência ao BigButton
    [SerializeField] private float animDuration;        // duração de uma animação 
    
    private float timeElapsed;                          // tempo decorrido a ser acumulado para atingir o tempo de animDuration

    private Vector3 startPos;                           // struct da posição inicial do botão (irá receber o startPosition passado por BigButton para o método HandleAnimation)
    private float finalPosY;                            // posição Y final do botão (irá receber o finalPositionY passado por BigButton para o método HandleAnimation)
    
    private bool isAnimating = false;                   // será true quando houver o toque (ver método HandleAnimation) e false no else do Update (fim de movimentação do botão)
    private int stepCounter = 0;                        // contador de movimentos do botão (2 movimentos equivalem a uma descida + subida)
    
    
    void Start()
    {
        bigButton.OnButtonPushAnimation += HandleAnimation;         // inscrição do método HandleAnimation no evento gerado por bigButton
        animDuration = 0.15f;                                       // valor da animação inicial 
    }

    private void HandleAnimation(Vector3 startPosition, float finalPositionY)       // método recebe parâmetros do bigButton
    {
        startPos = startPosition;                                                   // posição inicial do botão registrada
        finalPosY = finalPositionY;                                                 // posição Y final do botão registrada
        
        timeElapsed = 0f;                                                           // timeElapsed resetado
        isAnimating = true;                                                         // animação em andamento
        
        stepCounter = 0;                                                            // reset de reativações de movimentação do botão
    }

    void Update()
    {
        if (!isAnimating) return;                                                   // sem animação, não faz nada
        
        if (timeElapsed < animDuration)                                  // enquanto acumulador foi menor que a duração total da animação...
        {
            float t = timeElapsed / animDuration;                                   // passo t recebe fração referente à divisão
            t = t * t * (3f - 2f * t);                                              // equação de movimentação "smooth"
            
            float newY = Mathf.Lerp(startPos.y, finalPosY, t);                      // valor de interpolação salvo em newY

            // SWAP 
            Vector3 pos = transform.localPosition;                                  // struct pos registra posição local do botão
            pos.y = newY;                                                           // a mesma struct recebe em sua coordenada Y o valor newY 
            transform.localPosition = pos;                                          // swap finalizado com struct da posição local copiando valor da struct pos
            
            timeElapsed += Time.deltaTime;                                          // ao final, timeElapsed é incrementado com tempo passado no jogo
        }
        else                                                             // ... mas, se acumulador foi maior ou igual à duração total da animação...
        {
            timeElapsed = 0f;                                            // resetar tempo aqui
            (startPos.y, finalPosY) = (finalPosY, startPos.y);           // inverter posições "início X chegada"
            
            stepCounter++;                                               // registra movimentos do botão em um contador 

            if (stepCounter >= 2)                                        // 2 movimentos (descida + subida) equivalem a um ciclo completo de animação 
            {
                isAnimating = false;                                     // encerra animação (animação de descida e subida completa)
                stepCounter = 0;                                         // reseta contador de movimentos do botão
            }
        }
    }
}
