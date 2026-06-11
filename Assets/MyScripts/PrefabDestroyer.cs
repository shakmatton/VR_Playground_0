using System;
using UnityEngine;

// Versão 1: nave toca no asteroide e destrói ele (útil para futuros power-ups).
// Versão 2: nave toca no asteroide e destrói ele, e asteroide toca em asteroide e ambos são destruídos.
// Versão 3: nave atira no asteroide e destrói ele, e asteroide toca em asteroide e ambos são destruídos. 

namespace MyScripts
{
    public class PrefabDestroyer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;         // arrastar prefab que já tenha um tag próprio

        private void OnTriggerEnter(Collider other)         // usar isso em vez de OnCollisionTrigger, pois este último é indicado para objetos com interação física 
        {
            if (prefab == null) return;                  // evita problema de prefab no Inspector vazio/nulo (evita NullReferenceException, que nunca ativaria o destroy).
            
            if (other.CompareTag(prefab.tag))               // "other" é o objeto embutido em um script (spacechip ou asteroide)
            {                                               // comparação checa se o prefab (spaceship ou asteroide) entrou na zona de collider com outro prefab com tag (asteroide).
                Destroy(other.gameObject);                  // objeto prefab (asteroide) destruído
            }
        }
    }
}
