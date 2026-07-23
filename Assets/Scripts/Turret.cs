using System;
using UnityEngine;


public class Turret : Target
{
    [SerializeField] private int _hitsToKill = 2;
    [SerializeField] protected int damage = 10;

    public virtual void Attack(Health target)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(int amount)
    {
        _hitsToKill--;

        if (_hitsToKill <= 0)
        {
            Die();
        }

        Debug.Log("GorillaDamaged");
    }

    private void Die()
    {

        Debug.Log("GorillaDead");

        Destroy(gameObject);
    }
}
