using System;
using UnityEngine;

public class TankHealthComponent2 : MonoBehaviour, IDamageable
{
    [Header("체력 설정")]
    [SerializeField] int maxHp = 100;

    int currentHp;

    // 이벤트 정의
    public event Action<int, int> OnDamaged; // (현재체력, 최대체력)
    public event Action OnDied;

    // 프로퍼티 (외부에서 읽기 전용)
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    void Start()
    {
        currentHp = maxHp;
        Debug.Log($"{gameObject.name} 초기 체력: {currentHp}/{maxHp}");
    }

    public void TakeDamage(int damage)
    {
        currentHp = Math.Max(0, currentHp - damage);
        Debug.Log($"{gameObject.name} 데미지 {damage} 받음! 남은 체력: {currentHp}");

        // 이벤트 발생
        OnDamaged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        OnDied?.Invoke();
        Destroy(gameObject, .1f);
    }
}
