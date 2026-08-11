using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : Target
{

    public float shootInterval = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 30;

    public GameObject shootPoint;


    [SerializeField] private int _hitsToKill = 2;
    [SerializeField] protected int damage = 10;

    void Start()
    {
        StartCoroutine(shootCoro(shootInterval));
    }

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


    private IEnumerator shootCoro(float waitTime)
    {
        while (projectilePrefab != null)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("shooting");
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            
            Vector3 direction = (player.transform.position - shootPoint.transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.transform.position, Quaternion.Euler(direction));

            projectile.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;

        }
    }
}
