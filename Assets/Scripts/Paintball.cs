using UnityEngine;

public class Paintball : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Turret turret = collision.gameObject.GetComponent<Turret>();
        Target target = collision.gameObject.GetComponent<Target>();
        if (turret != null)
        {
            turret.TakeDamage(damage);
            Debug.Log("PaintballTryDamage");
            return;
        }
        if (target != null)
            target.DestroyTarget();

        Destroy(gameObject);
    }
}
