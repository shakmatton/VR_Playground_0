using System;
using MyScripts;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.5f;                          // velocidade do tiro
    [SerializeField] private float timeLimit = 3f;                              // tempo de existência do tiro

    private int points = 0;

    public event Action<int> OnCountChanged; 

    private void Start()
    { 
        Destroy(gameObject, timeLimit);                                         // certo tempo após o início da vida do tiro, ele é destruído 
    }                                                                           // não confundir com o método do ciclo de vida do Unity chamado "OnDestroy()" 
                                                                                // OnDestroy() ocorre só quando o objeto deixa de existir (diferente do uso explícito "Destroy" aqui)
                                                                                
    public void Increment()
    {
        points++;
        OnCountChanged?.Invoke(points);
    }
    
    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;          // tiro se move pra frente
    }
}




