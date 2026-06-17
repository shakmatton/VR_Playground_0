using System;
using System.Collections.Generic;
using UnityEngine;

// Versão 1: nave toca no asteroide e destrói ele (útil para futuros power-ups).
// Versão 2: nave toca no asteroide e destrói ele, e asteroide toca em asteroide e ambos são destruídos.
// Versão 3: nave atira no asteroide e destrói ele, e asteroide toca em asteroide e ambos são destruídos. 

namespace MyScripts
{
    public class PrefabDestroyer : MonoBehaviour
    {
        [SerializeField] private List<string> tagsToDestroy;                // lógica: criar um "pool" de tags destruíveis
        [SerializeField] private bool autoDestroy;                          // boolean configurado via Inspector
    
        private void OnTriggerEnter(Collider other)         // usar isso em vez de OnCollisionTrigger, pois este último é indicado para objetos com interação física 
        {
            if (tagsToDestroy.Contains(other.tag))          // se existir uma outra tag pertencente ao nosso conjunto de tags "tagsToDestroy"...
            {
                Destroy(other.gameObject);                  // ...objeto que colidir com este prefab corrente aqui é destruído
                
                if (autoDestroy)                            // se autodestruição estiver marcado como True...
                {
                    Destroy(gameObject);                    // ... aí é o prefab que contém esse script (tiro, asteroide) que se autodestroi.
                }                                           // obs.: no Inspector, asteroide tem "TagsToDestroy" (nave) e tem AutoDestroy ativado.
            }                                               // obs. 2: no Inspector, SpaceBullet (tiro) tem "TagsToDestroy" (asteroid e nave) e tem AutoDestroy ativado.
        }
    }
}
