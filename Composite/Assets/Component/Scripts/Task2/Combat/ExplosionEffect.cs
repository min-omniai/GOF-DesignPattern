using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] float duration = .5f;
    [SerializeField] float expandSpeed = 5f;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
    }
}
