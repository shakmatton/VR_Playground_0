using System;
using MyScripts;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.5f;                          // velocidade do tiro
    [SerializeField] private float timeLimit = 3f;                              // tempo de existência do tiro

    [SerializeField] private GameObject shotObject;                             // é o prefab de tiro da nave a ser gerado

    private void Start()
    { 
        Destroy(gameObject, timeLimit);                                         // certo tempo após o início da vida do tiro, ele é destruído 
    }                                                                           // não confundir com o método do ciclo de vida do Unity chamado "OnDestroy()" 
                                                                                // OnDestroy() ocorre só quando o objeto deixa de existir (diferente do uso explícito "Destroy" aqui)
    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;          // tiro se move pra frente
    }
}
