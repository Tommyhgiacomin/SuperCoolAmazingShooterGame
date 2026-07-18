using System;
using UnityEngine;

public class Turret : Enemy, IDamageable
{

    [SerializeField] private int _hitsToExplode = 2;
    [SerializeField] private int _explosionDamage = 40;
    [SerializeField] private float _explosionRadius = 3f;

    private bool _exploded;

    public bool IsDead => _exploded;

    public void TakeDamage(int amount)
    {
        if (_exploded) return;
        _hitsToExplode--;

        if (_hitsToExplode <= 0) Explode();
    }

    private void Explode()
    {
        _exploded = true;

        foreach (Collider nearby in Physics.OverlapSphere(transform.position, _explosionRadius))
        {
            if (nearby.TryGetComponent(out IDamageable target) && !target.IsDead)
            {
                target.TakeDamage(_explosionDamage);
            }
        }

        Destroy (gameObject);

    }
}
