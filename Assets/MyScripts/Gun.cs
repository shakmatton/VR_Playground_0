using UnityEngine;

public class Gun : MonoBehaviour
{
   public GameObject bulletPrefab;
   public Transform firePoint;
   public float bulletSpeed = 20f;
   public float bulletLifeTime = 5f;

   public void FireBullet()
   {
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
      Rigidbody rb = bullet.GetComponent<Rigidbody>();

      if (rb != null)
      {
         rb.linearVelocity = firePoint.right * bulletSpeed;       // Directions: Right: X | Forward: Z | Up: Y
      }
      
      Destroy(bullet, bulletLifeTime);
   }
}
