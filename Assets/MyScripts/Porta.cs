using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Porta : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [SerializeField] private float doorSpeed;               // graus por segundo
    [SerializeField] private float maxDoorAngle;            // ângulo máximo de abertura da porta
    private float currentAngle;                             // ângulo atual da porta
    private bool doorOpening;                               // estado de abertura da porta (fechada por padrão)

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();    
        
        currentAngle = 0f;
        maxDoorAngle = 90f;
        doorOpening = false;
        doorSpeed = 45f;
        
        // Debug.Log("PORTA INICIALIZADA!");
    }

    // Requer um Collider com "Is Trigger" marcado no Inspector
    private void OnTriggerEnter(Collider other)         // Evento de colisão dispara gatilho OnTriggerEnter: Collider com "isTrigger" habilitado
    {
        if (other.CompareTag("Player"))                 // Se colisão for com o jogador (cuja tag deve ser "Player")...
        {
            doorOpening = true;                         // Porta liberada para se movimentar.
            // Debug.Log("PORTA ABRIU!");
            
            /*
            ATENÇÃO: Debug.Log foi comentado para mitigar o problema abaixo:
            Ao caminhar, o jogador pode estar entrando e saindo do trigger repetidamente a cada frame
            (o collider "vibra" na borda da zona), causando lentidão na movimentação.
            
            Debug.Log é muito pesado quando chamado em alta frequência (é uma das causas mais comuns de lentidão no Editor da Unity).
            Lembrar de ter cuidado com isso!
            
            Sugestão: proteger o log com flag.
            
            private bool wasOpening = false; // flag nova
            [...]
            if (!wasOpening) // só loga se mudou de estado
               {
                   Debug.Log("PORTA ABRIU!");
                   wasOpening = true;
               }             
            */
        }
    }

    private void OnTriggerExit(Collider other)          // Evento de saída de colisão dispara gatilho OnTriggerExit: Collider com "isTrigger" habilitado
    {
        if (other.CompareTag("Player"))
        {
            doorOpening = false;                        // Porta travada para movimento.
            // Debug.Log("PORTA FECHOU!");
        }
    }

    private void Update()                                                           // lógica de animação da porta 
    {
        if (doorOpening && currentAngle < maxDoorAngle)                             // se a porta está liberada para movimento e o ângulo atual < máx...
        {
            float step = doorSpeed * Time.deltaTime;                                // step: quantidade de giro da porta no frame atual
            
            float remainder = maxDoorAngle - currentAngle;                          // remainder: o ângulo restante da diferença entre os ângulos max e atual
            
            step = Mathf.Min(step, remainder);                                      // step recebe o menor valor entre step e remainder (o que evita passar dos 90°)

            transform.Rotate(0f, 0f, -step, Space.Self);     // rotaciona com step negativo (porta "abre pra fora") no sistema de coordenadas Local
            
            currentAngle += step;                                                   // currentAngle é atualizado com a soma de si mesmo com o valor de Step
        }

        // Quando o jogador sair, a porta fica parada (doorOpening = false, nada acontece)
    }
}
 