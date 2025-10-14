using UnityEngine;

public class EnemyAIComponent : MonoBehaviour
{
    [Header("탐지 설정")]
    [SerializeField] float detectionRange = 15.0f;
    [SerializeField] float attackRange = 8.0f;

    [Header("이동 설정")]
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 50.0f;

    Transform player;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player 태그가 없습니다!");
        }
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // 탐지 거리: 추적
            ChasePlayer();
        }
        else if (distance <= attackRange)
        {
            // 공격 거리: 회전만
            RotateTowards(player.position);
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    void ChasePlayer()
    {
        // 플레이어 방향으로 회전
        RotateTowards(player.position);

        // 전진
        Vector3 moveDir = transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDir);
    }

    // 기즈모로 범위 시각화 (에디터에서 확인용)
    void OnDrawGizmosSelected()
    {
        // 탐지 범위 (노란색)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // 공격 범위 (빨간색)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
