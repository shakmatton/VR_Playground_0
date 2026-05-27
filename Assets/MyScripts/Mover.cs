using System;
using UnityEngine;

public class Mover : MonoBehaviour                                              // usado para mover os asteroides
{
    [SerializeField] private float speed = 2f;
    private void Update()                                                   
    {
        transform.position += transform.forward * speed * Time.deltaTime;       // movimento para a frente
    }
}
