using UnityEngine;
using System;

public class SpaceshipLimits : MonoBehaviour
{
    [SerializeField] private GameObject limitPointA;
    [SerializeField] private GameObject limitPointB;

    private Vector3 _spaceshipPosition;
    
    private float _minX, _maxX, _minY, _maxY;

    public static event Action<Vector3> OnSpaceshipOutOfBounds;
    
    // private Vector3 _limitA;
    // private Vector3 _limitB;


    private void Start()
    {
        Vector3 posA = limitPointA.transform.position;
        Vector3 posB = limitPointB.transform.position;
        
        // _spaceshipPosition = GetComponentInParent<Transform>().position;

        _minX = Mathf.Min(posA.x, posB.x);
        _maxX = Mathf.Max(posA.x, posB.x);
        
        _maxY = Mathf.Max(posA.y, posB.z);
        _minY = Mathf.Min(posA.y, posB.z);
        
        // _limitA = _limitPoint1.transform.localPosition; 
        // _limitB = _limitPoint2.transform.localPosition;
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
    
    
    // private void OnSpaceshipOutOfBounds()
    // {
    //     if (_spaceshipPosition.x < _limitA.x || _spaceshipPosition.x > _limitB.x || _spaceshipPosition.y > _limitB.y || _spaceshipPosition.y < _limitA.y)

    private void Update()
    {
        CheckBounds();
        // _spaceshipPosition = GetComponentInParent<Transform>().position;
    }
}
