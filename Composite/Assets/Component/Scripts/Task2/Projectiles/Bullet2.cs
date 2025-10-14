using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] int damage = 10;
    [SerializeField] float lifetime = 3.0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 인터페이스로 체크 (TryGetComponent)
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
