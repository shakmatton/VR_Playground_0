using System;
using System.Collections.Generic;
using UnityEngine;

// Colisão deve disparar evento OnCollide, que será usado posteriormente por método OnBulletCollide do SpaceBullet.cs.

namespace MyScripts
{
    public class PrefabDestroyer : MonoBehaviour
    {
        [SerializeField] private List<string> tagsToDestroy;                // lógica: criar um "pool" de tags de objetos destrutíveis
        [SerializeField] private bool autoDestroy;                          // boolean configurado via Inspector

        public event Action OnCollide;                                      // evento irá avisar que houve colisão (será aproveitado pelo SpaceBullet.cs)
                                                                            // Obs.: uma Action funciona como um array de ponteiros para métodos que nele se inscrevem
        
        private void OnTriggerEnter(Collider other)         // usar isso em vez de OnCollisionTrigger, pois este último é indicado para objetos com interação física 
        {
            // Debug.Log($"Trigger disparado: {gameObject.name} colidiu com {other.gameObject.name} (tag: {other.tag})");
            
            
            if (tagsToDestroy.Contains(other.tag))          // se a tag do objeto que colide pertence ao conjunto de tags "tagsToDestroy"...
            {   
                
                OnCollide?.Invoke();                        // disparo de evento de colisão (método de SpaceBullet irá se inscrever aqui)
                                                            // OnCollide é um array de ponteiros para métodos
                
                Destroy(other.gameObject);                  // ...objeto que colidir com este prefab corrente aqui é destruído
                
                if (autoDestroy)                            // se autodestruição estiver marcado como True...
                {
                    Destroy(gameObject);                    // ... aí é o prefab que contém esse script (tiro, asteroide) que se autodestroi.
                    
                }                                           // Asteroide -> "TagsToDestroy" (nave).
            }                                               // Asteroide -> autoDestroy ativado (ver Inspector).
                                                            
                                                            // SpaceBullet (tiro) -> "TagsToDestroy" (asteroid e nave).
                                                            // SpaceBullet -> autoDestroy ativado (ver Inspector).
        }
    }
}
