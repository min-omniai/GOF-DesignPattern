using UnityEngine;

public class AutoShootComponent3 : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] WeaponStatsSO weaponStats;     // S0 참조
    [SerializeField] AIStatsSO aiStats;     // 사거리용

    [Header("설정")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    Transform player;
    float nextFireTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (weaponStats == null)
        {
            Debug.LogError($"{gameObject.name}에 WeaponStats가 없습니다!");
        }
    }

    void Update()
    {
        if (player == null || bulletPrefab == null || firePoint == null || weaponStats == null || aiStats == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= aiStats.attackRange && Time.time >= nextFireTime)      // S0 사용
        {
            Shoot();
            nextFireTime = Time.time + weaponStats.fireRate;    // S0 사용
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet3 bulletScript = bullet.GetComponent<Bullet3>();
        if(bulletScript != null)
        {
            bulletScript.Initialize(weaponStats.damage, weaponStats.bulletSpeed);
        }
    }
}
