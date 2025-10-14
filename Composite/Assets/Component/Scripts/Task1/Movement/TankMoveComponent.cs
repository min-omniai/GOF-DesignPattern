using UnityEngine;
using UnityEngine.UIElements;

/*
방향키 WS 이동, AD 회전
Space 총알 발사
*/
public class TankMoveComponent : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 100f;

    Rigidbody rb = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * vertical * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Rotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float rotation = horizontal * rotateSpeed * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0, rotation, 0);
        rb.MoveRotation(rb.rotation * turn);
    }
}
