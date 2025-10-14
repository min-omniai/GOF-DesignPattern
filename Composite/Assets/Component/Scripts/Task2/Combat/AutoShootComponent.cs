using UnityEngine;

public class AutoShootComponent : MonoBehaviour
{
    [Header("발사 설정")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float attackRange = 8f;
    
    Transform player;
    float nextFireTime;
    
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
    
    void Update()
    {
        if (player == null || bulletPrefab == null || firePoint == null)
            return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        
        // 사거리 안에 있고, 쿨다운 끝났으면 발사
        if (distance <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}