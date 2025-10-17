using System;
using UnityEngine;

public class TankHealthComponent3 : MonoBehaviour, IDamageable
{
    [Header("데이터")]
    [SerializeField] TankStatsSO stats;         // S0 참조

    int currentHp;

    public event Action<int, int> OnDamaged;
    public event Action OnDied;

    public int CurrentHp => currentHp;
    public int MaxHp => stats.maxHp;        // S0에서 가져옴

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (stats == null)
        {
            Debug.LogError(($"{gameObject.name}에 TankStats가 없습니다!"));
            return;
        }

        currentHp = stats.maxHp;
        Debug.Log($"{gameObject.name} 초기 체력: {currentHp}/{stats.maxHp}");
    }

    public void TakeDamage(int damage)
    {
        currentHp = Math.Max(0, currentHp - damage);
        Debug.Log($"{gameObject.name} 데미지 {damage} 받음! 남은 체력: {currentHp}");

        OnDamaged?.Invoke(currentHp, stats.maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        OnDied?.Invoke();
        Destroy(gameObject, 0.1f);
    }
}
