using UnityEngine;

public enum BulletType
{
    Paintball,
    Sniper,
    Shotgun,
    GrenadeLauncher
}

public class WeaponSystem : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject paintballPrefab;
    public GameObject grenadePrefab;

    [Header("Paintball Settings")]
    public float paintballSpeed = 30f;

    [Header("Sniper Settings")]
    public float sniperRange = 200f;
    public LayerMask targetLayer;

    [Header("Shotgun Settings")]
    public int shotgunPellets = 8;
    public float shotgunSpreadAngle = 15f;

    [Header("Grenade Settings")]
    public float grenadeSpeed = 20f;

    [Header("Fire Rate")]
    public float fireRate = 0.2f;

    [Header("Wwise Events")]
    public AK.Wwise.Switch weaponSwitch;

    float nextFireTime;
    BulletType currentBulletType = BulletType.Paintball;
    ActiveGrenade activeGrenade;

    float perkTimer;
    BulletType defaultType = BulletType.Paintball;

    Collider playerCollider;

    public BulletType CurrentBulletType => currentBulletType;

    void Start()
    {
        playerCollider = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive) return;

        if (currentBulletType != defaultType && perkTimer > 0f)
        {
            perkTimer -= Time.deltaTime;
            if (perkTimer <= 0f)
                currentBulletType = defaultType;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentBulletType == BulletType.GrenadeLauncher && activeGrenade != null)
            {
                activeGrenade.Explode();
                activeGrenade = null;
                return;
            }

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Fire();
            }
        }
    }

    void Fire()
    {
        switch (currentBulletType)
        {
            case BulletType.Paintball:
                FirePaintball();
                break;
            case BulletType.Sniper:
                FireSniper();
                break;
            case BulletType.Shotgun:
                FireShotgun();
                break;
            case BulletType.GrenadeLauncher:
                FireGrenade();
                break;
        }

        AudioManager.Instance.PlayerFire(gameObject);
    }

    void FirePaintball()
    {
        GameObject ball = Instantiate(paintballPrefab, firePoint.position, firePoint.rotation);
        IgnorePlayerCollision(ball);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * paintballSpeed;
    }

    void FireSniper()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, sniperRange, targetLayer))
        {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
                target.DestroyTarget();
        }
    }

    void FireShotgun()
    {
        for (int i = 0; i < shotgunPellets; i++)
        {
            GameObject ball = Instantiate(paintballPrefab, firePoint.position, firePoint.rotation);
            IgnorePlayerCollision(ball);
            Rigidbody rb = ball.GetComponent<Rigidbody>();

            Vector3 spread = firePoint.forward;
            spread += firePoint.right * Random.Range(-shotgunSpreadAngle, shotgunSpreadAngle) / 100f;
            spread += firePoint.up * Random.Range(-shotgunSpreadAngle, shotgunSpreadAngle) / 100f;

            rb.linearVelocity = spread.normalized * paintballSpeed;
        }
    }

    void FireGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        IgnorePlayerCollision(grenade);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * grenadeSpeed;

        activeGrenade = grenade.GetComponent<ActiveGrenade>();
        activeGrenade.weaponSystem = this;
    }

    public void ClearActiveGrenade()
    {
        activeGrenade = null;
    }

    void IgnorePlayerCollision(GameObject projectile)
    {
        if (playerCollider == null) return;
        Collider projCol = projectile.GetComponent<Collider>();
        if (projCol != null)
            Physics.IgnoreCollision(projCol, playerCollider);
    }

    public void SetBulletType(BulletType type, float duration)
    {
        currentBulletType = type;
        perkTimer = duration;
    }
}
