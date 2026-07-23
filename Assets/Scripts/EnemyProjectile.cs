using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    [SerializeField] protected int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Health target = collision.gameObject.GetComponent<Health>();
        if (target != null)
            target.TakeDamage(damage);

        Destroy(gameObject);
    }
}
