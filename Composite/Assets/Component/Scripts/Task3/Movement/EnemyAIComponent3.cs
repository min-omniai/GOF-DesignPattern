using UnityEngine;

public class EnemyAIComponent3 : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] AIStatsSO aiStats;     // S0 참조

    Transform player;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError($"{gameObject.name}에 Rigidbody가 없습니다!");

        if (aiStats == null)
            Debug.LogError($"{gameObject.name}에 AIStats가 없습니다!");
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log($"{gameObject.name}이(가) 플레이어를 찾았습니다: {player.name}");
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트가 없습니다!");
        }
    }

    void FixedUpdate()
    {
        if (player == null || rb == null || aiStats == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= aiStats.attackRange)        // S0 사용
            RotateTowards(player.position);
        else if (distance <= aiStats.detectionRange)     // S0 사용
            ChasePlayer();
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if(direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.RotateTowards(
                rb.rotation,
                targetRotation,
                aiStats.rotationSpeed * Time.fixedDeltaTime     // S0 사용
            ));
        }
    }

    void ChasePlayer()
    {
        RotateTowards(player.position);

        Vector3 moveDir = transform.forward * aiStats.moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDir);
    }

    void OnDrawGizmosSelected()
    {
        if (aiStats == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aiStats.detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aiStats.attackRange);
    }
}
