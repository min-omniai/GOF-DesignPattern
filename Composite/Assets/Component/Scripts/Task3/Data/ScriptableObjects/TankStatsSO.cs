using UnityEngine;

[CreateAssetMenu(fileName = "TankStatsSO", menuName = "Tank/Stats", order = 0)]
public class TankStatsSO : ScriptableObject
{
    [Header("체력")]
    public int maxHp = 100;

    [Header("이동")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;

    [Header("설명")]
    [TextArea(3, 5)]
    public string description = "탱크 설명";
}
