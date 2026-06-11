using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.5f;                          // velocidade do tiro
    [SerializeField] private float timeLimit = 3f;                              // tempo de existência do tiro

    private void Start()
    {
        transform.position = GetComponent<Transform>().position;                // tiro parte da posição da nave (bico da nave)
    }
    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;          // tiro se move pra frente
    }
    private void OnDestroy()                                                    // após tempo-limite, destruir objeto SpaceBullet
    {
        Destroy(gameObject, timeLimit);
    }
}
