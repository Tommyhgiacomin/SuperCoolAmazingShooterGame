using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    protected Health health;

    [SerializeField] protected int damage = 10;

    public bool IsDead => health.IsDead;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
    }

    public virtual void Attack(Health target)
    {
        target.TakeDamage(damage);
    }

}
