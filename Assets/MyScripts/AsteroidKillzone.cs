using System;
using UnityEngine;

namespace MyScripts
{
    public class AsteroidKillzone : MonoBehaviour       
    
    /* KillZone possui um Box Collider com isTrigger ativado.    
       LEMBRETE: adicionar Sphere Collider no Asteroid e adicionar Rigidbody (com isKinematic ativado e isGravity desativado) */
    
    {
        private void OnTriggerEnter(Collider other)     // Método do Unity para colisões
        {
            // Método "Tag" usado abaixo. Mas, poderia ter sido usado Layers também (ver matriz de colisões, em Project Settings > Physics > Settings).
            
            if (other.gameObject.tag == "asteroid")    // Killzone deve "matar" apenas asteroides, em vez de objetos diversos...
            {
                Destroy(other.gameObject);             // Destrói o gameObject que entra em contato com o Killzone.
            }
        }
    }
}