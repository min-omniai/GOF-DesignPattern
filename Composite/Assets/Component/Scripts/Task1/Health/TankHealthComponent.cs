using UnityEngine;

public class TankHealthComponent : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] int maxHp = 100;

    int currentHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
        Debug.Log($"{gameObject.name} 체력: {currentHp}/{maxHp}");
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name} 데미지 {damage} 받음! 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 파괴됨!");
        Destroy(gameObject);
    }
}
