using UnityEngine;

[CreateAssetMenu(fileName = "AIStats", menuName = "Tank/AI Stats", order = 2)]
public class AIStatsSO : ScriptableObject
{
    [Header("탐지")]
    public float detectionRange = 15.0f;
    public float attackRange = 8.0f;

    [Header("이동")]
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 50.0f;

    [Header("설명")]
    [TextArea(3, 5)]
    public string description = "AI 설명";
}
