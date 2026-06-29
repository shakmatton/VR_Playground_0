using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyScripts
{
    public class PrefabDestroyer : MonoBehaviour
    {
        [SerializeField] private List<string> tagsToDestroy;                // lógica: criar um "pool" de tags de objetos destrutíveis
        [SerializeField] private bool autoDestroy;                          // boolean configurado via Inspector
        
        
        
        private void OnTriggerEnter(Collider other)         // usar isso em vez de OnCollisionTrigger, pois este último é indicado para objetos com interação física 
        {
            if (tagsToDestroy.Contains(other.tag))          // se a tag do objeto que colide pertence ao conjunto de tags "tagsToDestroy"...
            {
                Destroy(other.gameObject);                  // ...objeto que colidir com este prefab corrente aqui é destruído
                
                if (autoDestroy)                            // se autodestruição estiver marcado como True...
                {
                    Destroy(gameObject);                    // ... aí é o prefab que contém esse script (tiro, asteroide) que se autodestroi.
                    
                }                                           // Asteroide: "TagsToDestroy" (nave) e autoDestroy ativado (ver Inspector).
                                                            // SpaceBullet (tiro): "TagsToDestroy" (asteroid e nave) e autoDestroy ativado (ver Inspector).
            }
        }
    }
}
