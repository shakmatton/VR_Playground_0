using UnityEngine;

public class PiaoNumbers : MonoBehaviour
{
    [SerializeField] private float piaoSpeed = 200f; // graus por segundo
    private bool isSpinning = false;

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(0f, piaoSpeed * Time.deltaTime, 0f, Space.Self);
        }
    }

    public void StartSpinning()
    {
        isSpinning = true;
    }

    public void StopSpinning()
    {
        isSpinning = false;
    }
}


// using UnityEngine;
//
// public class PiaoNumbers : MonoBehaviour
// {
//     
//     [SerializeField] private float piaoSpeed = 1.0f;
//     private float rotationY;
//     
//     void Start()
//     {
//         rotationY = gameObject.transform.rotation.y;
//     }
//
//     public void StartSpinning()
//     {
//         rotationY += piaoSpeed * Time.deltaTime;
//     }
//     public void StopSpinning()
//     {
//         rotationY -= piaoSpeed * Time.deltaTime;
//         if (rotationY < 0)
//         {
//             rotationY = 0;
//         }
//     }
// }
//
//
