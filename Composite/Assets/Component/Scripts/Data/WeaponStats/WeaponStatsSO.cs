using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Tank/Weapon Stats", order = 1)]
public class WeaponStatsSO : ScriptableObject
{
    [Header("공격")]
    public int damage = 10;
    public float fireRate = 0.5f;

    [Header("투사체")]
    public float bulletSpeed = 20.0f;

    [Header("설명")]
    [TextArea(3, 5)]
    public string description = "무기 설명";
}
