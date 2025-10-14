using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] int damage = 10;
    [SerializeField] float lifetime = 3.0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 체력 컴포넌트 찾기
            if(other.TryGetComponent<TankHealthComponent>(out var health))
            {
                health.TakeDamage(damage);
            }

            // 총알 파괴
            Destroy(gameObject);
        }
    }
}
