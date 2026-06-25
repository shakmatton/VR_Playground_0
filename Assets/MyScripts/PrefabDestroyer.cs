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
        [SerializeField] private List<string> tagsToDestroy;                // lógica: criar um "pool" de tags de objetos destrutíveis
        [SerializeField] private bool autoDestroy;                          // boolean configurado via Inspector

        private int points = 0;
        
        [SerializeField] private bool triggerGameOver;                      // Será a tela de UI de GameOver. Configurar no Inspector com true no asteroide e false no tiro.
                                                                            // Isso permite reusar o mesmo script com comportamentos diferentes, sem duplicar código.
        private void OnTriggerEnter(Collider other)         // usar isso em vez de OnCollisionTrigger, pois este último é indicado para objetos com interação física 
        {
            if (tagsToDestroy.Contains(other.tag))          // se a tag do objeto que colide pertence ao conjunto de tags "tagsToDestroy"...
            {
                Destroy(other.gameObject);                  // ...objeto que colidir com este prefab corrente aqui é destruído
                
                if (autoDestroy)                            // se autodestruição estiver marcado como True...
                {
                    Destroy(gameObject);                    // ... aí é o prefab que contém esse script (tiro, asteroide) que se autodestroi.
                    
                                                            // nave e asteroide: autoDestroy ativado. 
                }                                           // Inspector: asteroide com "TagsToDestroy" (nave).
                                                            // Inspector: SpaceBullet (tiro) com "TagsToDestroy" (asteroid e nave).
            }
            if (triggerGameOver)                            // apenas a nave possui triggerGameOver ativado por padrão (asteroide e tiro têm essa variável sempre desativada)
            {
                UI_Manager.Instance.ShowGameOver();         // no método OnTriggerEnter, a nave irá avisar ao UI_Manager que deve mostrar a tela de GameOver
            }

            // points++;           
            //
            // if (points >= 3)
            // {
            //     UI_Manager.Instance.ShowVictory();
            // }
        }
    }
}
