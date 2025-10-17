using UnityEngine;
using UnityEngine.UIElements;

public class TankMoveComponent3 : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] TankStatsSO stats;     // S0 참조

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(stats == null)
        {
            Debug.LogError($"{gameObject.name}에 TankStats가 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stats == null) return;

        Move();
        Rotate();
    }

    void Move()
    {
        float vertical = Input.GetAxis("Verticle");
        Vector3 movement = transform.forward * vertical * stats.moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
    
    void Rotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float rotation = horizontal * stats.rotationSpeed * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0, rotation, 0);
        rb.MoveRotation(rb.rotation * turn);
    }
}
