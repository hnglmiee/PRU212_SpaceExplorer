using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
