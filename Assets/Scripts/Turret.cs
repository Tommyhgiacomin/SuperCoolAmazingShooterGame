using System;
using UnityEngine;

public class Turret : Enemy, IDamageable
{

    [SerializeField] private int _hitsToKill = 2;

    private bool _exploded;

    public void TakeDamage(int amount)
    {
        if (IsDead) return;
        _hitsToKill--;

        if (_hitsToKill <= 0) Die();
    }

    private void Die()
    {
        _exploded = true;

        Destroy (gameObject);

    }
}
