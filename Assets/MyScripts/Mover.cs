using System;                          // System.Random embutido (ambiguação com UnityEngine.Random)
using UnityEngine;                     // UnityEngine.Random embutido (ambiguação com System.Random)
using Random = UnityEngine.Random;     // criação de alias (apelido) para desambiguação

// Vide comentário inicial em Spawner.cs também.

/* Este script usa a linha "using Random = UnityEngine.Random" porque importa tanto "System" quanto "UnityEngine" por completo.
   Ao fazer isso, duas classes chamadas "Random" passam a existir no mesmo escopo: System.Random e UnityEngine.Random. 
   O compilador não consegue decidir qual delas usar e lança um erro de referência ambígua. O alias resolve o problema
   ao instruir o compilador: "sempre que eu escrever Random neste script, entenda que me refiro à UnityEngine.Random". */


public class Mover : MonoBehaviour                                      // Para cada asteroide, enquanto ele ainda estiver "vivo", ele se move.
                                                                        // Este é o ciclo de vida de movimentação dele.
{
    [SerializeField] private float speed = -0.75f;                      // Valor padrão de velocidade
    [SerializeField] private float acceleration = 1f;                   // Valor padrão de aceleração (cada asteroide começa com esse valor padrão)
    
    private void Start()
    {
        acceleration = Random.Range(acceleration, acceleration + 2f);   // Valor padrão de aceleração do asteroide é mudado uma única vez, logo que começa a se mover.
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * acceleration * Time.deltaTime);     // Asteroide se move para a frente.
                                                                                               // Movimento usa últimos valores definidos: velocidade e aceleração.
    }
}


