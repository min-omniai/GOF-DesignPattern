using UnityEngine;

public class DeathEffectComponent : MonoBehaviour
{
    [Header("이펙트 설정")]
    [SerializeField] GameObject explosionPrefab;

    TankHealthComponent2 health;

    void Awake()
    {
        health = GetComponent<TankHealthComponent2>();
    }

    void Start()
    {
        if (health != null)
        {
            health.OnDied += OnDiedHandler;
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDied -= OnDiedHandler;
        }
    }

    void OnDiedHandler()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
