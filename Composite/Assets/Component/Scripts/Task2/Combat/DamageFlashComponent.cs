using System.Collections;
using UnityEngine;

public class DamageFlashComponent : MonoBehaviour
{
    [Header("플래시 설정")]
    [SerializeField] Color flashColor = Color.red;
    [SerializeField] float flashDuration = .2f;

    TankHealthComponent2 health;
    Renderer rend;
    Color originalColor;

    void Awake()
    {
        health = GetComponent<TankHealthComponent2>();
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    void Start()
    {
        if (health != null)
        {
            health.OnDamaged += OnDamagedHandler;
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDamaged -= OnDamagedHandler;
        }
    }

    void OnDamagedHandler(int currentHp, int maxHp)
    {
        if (rend != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }
}
