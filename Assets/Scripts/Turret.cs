using System;
using UnityEngine;

public class Turret : Target, IDamageable
{

    [SerializeField] private int _hitsToKill = 2;

    private bool _died;

    public void TakeDamage(int amount)
    {
        if (IsDead) return;
        _hitsToKill--;

        if (_hitsToKill <= 0) Die();
    }

    private void Die()
    {
        _died = true;

        Destroy (gameObject);

    }
}
