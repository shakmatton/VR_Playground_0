using UnityEngine;
using System;

/* Esse script é complexo, mas poderia funcionar com LateUpdate ou enviando o objeto para outro script (NewSpaceshipController.cs, via SerializeField, por exemplo).
   Aqui, estamos usando Eventos, mas usar isso aqui era exagero... não precisa praticar isso aqui.
   
   A natureza de usar Eventos diz respeito a ter uma mensagem disparada anonimamente. Isso é útil para quando temos muitos objetos com comportamentos diferentes. 
   Por exemplo, se um jogo tipo "Pong" fosse misturado com a cena dos asteroides, poderia ser interessante usar eventos aqui.
      
   Em relação ao script, a ideia era a de modularizar as coisas ("cada um no seu quadrado": NewSpaceShipController.cs controla nave, SpaceshipLimits.cs detecta bordas).
   Porém, para um treino inicial, foi aceitável inserir o comportamento do script SpaceshipLimits.cs dentro de NewSpaceShipController.cs.
   Portanto, esse script foi desativado no Inspector, e sua lógica foi replicada dentro de NewSpaceshipController.cs  */


/*
public class SpaceshipLimits : MonoBehaviour
{
    [SerializeField] private GameObject limitPointA;
    [SerializeField] private GameObject limitPointB;

    private Vector3 _spaceshipPosition;
    
    private float _minX, _maxX, _minY, _maxY;

    public static event Action<Vector3> OnSpaceshipOutOfBounds;
    
    private void Start()
    {
        Vector3 posA = limitPointA.transform.position;
        Vector3 posB = limitPointB.transform.position;
        
        _minX = Mathf.Min(posA.x, posB.x);
        _maxX = Mathf.Max(posA.x, posB.x);
        
        _maxY = Mathf.Max(posA.z, posB.z);
        _minY = Mathf.Min(posA.z, posB.z);
    }

    private void CheckBounds()
    {
        Vector3 currentPos = transform.position;

        float clampedX = Mathf.Clamp(currentPos.x, _minX, _maxX);                 // retorna o valor mínimo, o próprio valor, ou o máximo
        float clampedY = Mathf.Clamp(currentPos.z, _minY, _maxY);                 // em vez de vários if/else, Clamp garante que o valor fica entre min e max.

        bool outOfBounds = !Mathf.Approximately(clampedX, currentPos.x) ||        // Compara floats com tolerância. Nunca se deve comparar floats com == diretamente no Unity.
                           !Mathf.Approximately(clampedY, currentPos.z);

        if (outOfBounds)
        {
            Vector3 clampedPosition = new Vector3(clampedX, currentPos.y, clampedY);
            OnSpaceshipOutOfBounds?.Invoke(clampedPosition);
        }
    }
    
    private void Update()
    {
        CheckBounds();
    }
}
*/