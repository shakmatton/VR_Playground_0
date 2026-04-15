using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace MyScripts
{
    public class LeverInteractable : XRSimpleInteractable                        // usando o simpleInteractable por agora
    {
        private Vector3 pos;
        private Vector3 dir;
        private float clampedAngle;                                              // usado como apoio no método "oscillator.RestartOscillator(clampedAngle);"
        
        [SerializeField] private Oscillator oscillator;                          // referência para esse outro script
        
        protected override void OnSelectEntered(SelectEnterEventArgs args)       // método ativa interação
        {
            base.OnSelectEntered(args);                                          // interação ativada via método homônimo do pai 
            Debug.Log("Interact");
            oscillator.enabled = false;                                          // desativa a animação do oscillator (para permitir interação manual da alavanca)
        }

        protected override void OnSelectExited(SelectExitEventArgs args)         // método desativa interação
        {
            base.OnSelectExited(args);
            Debug.Log("Not Interact");
            // oscillator.enabled = true;
            oscillator.RestartOscillator(clampedAngle);                          // método para substituir o "oscillator.enabled = true"
        }
    
        private void OnDrawGizmos()                                              // método de teste (debug gráfico)
        {
            if (!isSelected)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, 0.1f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, dir * 10);
        }
        
        private void Update()
        {
            if (isSelected)                         // WARNING: linear algebra ahead!
            {
                Debug.Log("Is Selected");
                
                // pos recebe a posição do attach transform do interactor atual
                pos = firstInteractorSelecting.GetAttachTransform(this).position;            
                
                // dir recebe a direção normalizada de transform até pos (apenas direção, sem magnitude): direção do vetor (pos final - pos inicial, em módulo)
                // lembrar da diferença entre transform.position e transform.localPosition (para saber qual dimensão operar vetores)
                // usar normalized sempre que quisermos obter apenas a direção (pois é mais simples que lidar com vetores de tamanhos aleatórios).
                dir = (pos - transform.position).normalized;                                            
                
                // Projeta dir no plano cujo normal é o forward do pai, eliminando qualquer componente fora desse plano
                // dir é atualizado com vetor resultante da projeção de "dir original" sobre o eixo Z do objeto pai (a normal do plano do objeto Lever)
                // não usar eixo Z do próprio objeto que está rotacionando!
                dir = Vector3.ProjectOnPlane(dir, transform.parent.forward);      
                
                // Mede o ângulo com sinal (+ ou -) formado entre parent.up e dir, girando em torno do eixo parent.forward
                // angle recebe um Vector3 com ângulo determinado pela conjunção do objeto pai no eixo Y e Z com a direção determinada pelo plano do vetor dir.
                float angle = Vector3.SignedAngle(transform.parent.up, dir, transform.parent.forward);
                
                // Trava o ângulo entre -45 e 45 graus
                clampedAngle = Mathf.Clamp(angle, -45f, 45f);

                // Reconstrói dir a partir do parent.up rotacionado por clampedAngle em torno do parent.forward
                // Ou seja, reconstrói a direção travada no passo anterior (trava de alcance entre -45 e 45 graus).
                // "Take parent.up, and rotate it by clampedAngle degrees around parent.forward"
                dir = Quaternion.AngleAxis(clampedAngle, transform.parent.forward) * transform.parent.up;

                /* Diferença entre SignedAngle e AngleAxis (um é reverso do outro):
                        SignedAngle — took dir and extracted the angle relative to parent.up
                        AngleAxis * parent.up — takes the angle and reconstructs dir relative to parent.up   */
                
                // Cria rotação que gira o eixo parent.up até apontar para dir, e aplica isso como a rotação absoluta do transform.
                transform.rotation = Quaternion.FromToRotation(transform.parent.up, dir);
            }
        }
    }
}


/* Solução da IA:

       public class LeverInteractable : XRSimpleInteractable
       {
           [Header("Alavanca")]
           [SerializeField] private float minAngle = -60f;
           [SerializeField] private float maxAngle = 60f;
           [SerializeField] private Vector3 rotationAxis = Vector3.forward; // eixo Z = pendular
   
           private Vector3 pos;
           private Oscillator oscillator;
   
           protected override void Start()
           {
               base.Start();
               oscillator = GetComponent<Oscillator>();
           }
   
           protected override void OnSelectEntered(SelectEnterEventArgs args)
           {
               base.OnSelectEntered(args);
               if (oscillator != null)
                   oscillator.activatedInteraction = false;
           }
   
           protected override void OnSelectExited(SelectExitEventArgs args)
           {
               base.OnSelectExited(args);
               if (oscillator != null)
                   oscillator.activatedInteraction = true;
           }
   
           private void Update()
           {
               if (!isSelected) return;
   
               pos = firstInteractorSelecting.GetAttachTransform(this).position;
               RotateTowardsHand();
           }
   
           private void RotateTowardsHand()
           {
               Vector3 directionToHand = pos - transform.position;
   
               Vector3 projected = Vector3.ProjectOnPlane(directionToHand, transform.TransformDirection(rotationAxis));
               if (projected == Vector3.zero) return;
   
               float angle = Vector3.SignedAngle(
                   transform.parent ? transform.parent.up : Vector3.up,
                   projected,
                   transform.TransformDirection(rotationAxis)
               );
   
               angle = Mathf.Clamp(angle, minAngle, maxAngle);
               transform.localRotation = Quaternion.AngleAxis(angle, rotationAxis);
           }
   
           private void OnDrawGizmos()
           {
               if (!isSelected) return;
               Gizmos.color = Color.red;
               Gizmos.DrawWireSphere(pos, 0.1f);
           }
       }
   }

*/