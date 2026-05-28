using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Subnamespaces específicos de System (Collections e Collections.Generic) importados, mas nunca foi importado o System diretamente.
// Como a classe System.Random mora no namespace raiz System, ela nunca entra em cena. O compilador vê apenas uma classe chamada Random — a do UnityEngine — e não há conflito.

/* Este script usa "Random.Range" sem precisar do alias "using Random = UnityEngine.Random" (vide Mover.cs) porque ele nunca importa o namespace "System" diretamente.
   Os namespaces importados — System.Collections e System.Collections.Generic — são subnamespaces específicos, e nenhum deles carrega a classe System.Random para o escopo.
   Assim, quando o compilador lê a palavra "Random", só existe uma classe com esse nome disponível: a UnityEngine.Random. Sem ambiguidade, sem conflito. */

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject obj;     // obj = futuro objeto prefab (a ser arrastado no Inspector)
    [SerializeField] private float dist;         // distância entre os objetos que surgem na tela 
    
    private List<Vector3> positions = new List<Vector3>();      // array de structs criado: uma lista de posições, representando esquerda, meio e direita 
    
    void Start()
    {
        for (int x = 0; x <= 2; x++)                            // 3 posições (esquerda, meio, direita)
        {
            positions.Add(new Vector3(x * dist, 0, 0));         // para cada posição, eixo x será deslocado "dist" vezes
        }

        StartCoroutine(SpawnRoutine());                         // Coroutine chamada logo no Start, ativando o método SpawnRoutine
    }

    private IEnumerator SpawnRoutine()                          // Método SpawnRoutine é um IEnumerator (funciona como um Update)
    {                                                           // Funciona de modo similar a uma Thread
        while (true)                                                        // Executará "para sempre" (yield return pausará o loop para permitir que outras coisas ocorram)
        {
           GameObject objSpawned = Instantiate(obj, transform);             // modo de fazer um objeto prefab ser instanciado a partir do obj (modelo) e sua posição do transform
           int index = Random.Range(0,3);                                   // índice recebe um valor no Range de 0 a 2
           
           Vector3 position = positions[index];                             // um novo struct recebe uma struct da lista de Vector3 (criada anteriormente) na posição "index" 
           objSpawned.transform.localPosition = position;                   // posição do prefab será dada pelo novo struct "position"
           
           // Debug.Log("Spawned...");
           
           yield return new WaitForSeconds(Random.Range(1,3));              // modo de fazer com que o loop pause em quantidade X de tempo (determinada por valores escolhidos)
        }
    }                                                                       
}
