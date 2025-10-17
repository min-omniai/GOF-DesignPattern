using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    int damage = 10;
    float speed = 20.0f;
    [SerializeField] float lifetime = 3.0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(int dmg, float spd)
    {
        damage = dmg;
        speed = spd;
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == transform.parent) return;

        if(other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
