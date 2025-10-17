using UnityEngine;

public class TankShootComponent3 : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] WeaponStatsSO weaponStats;

    [Header("설정")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    float nextFireTime;

    void Update()
    {
        if (weaponStats == null) return;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + weaponStats.fireRate;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("총알 프리팹 또는 발사 위치가 없습니다!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet3 bulletScript = bullet.GetComponent<Bullet3>();
        if(bulletScript != null)
        {
            bulletScript.Initialize(weaponStats.damage, weaponStats.bulletSpeed);
        }
    }
}
