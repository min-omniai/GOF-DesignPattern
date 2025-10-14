using UnityEngine;

public class TankShootComponent : MonoBehaviour
{
    [Header("발사 설정")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = .5f;

    float nextFireTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("총알 프리팹 또는 발사 위치가 없습니다!");
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
