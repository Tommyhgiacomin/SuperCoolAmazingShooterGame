using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    [SerializeField] protected int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        Health target = other.GetComponent<Health>();
        if (target != null)
            target.TakeDamage(damage);

        Debug.Log("BananaTouchPlayer");

        Destroy(gameObject);
    }

}
