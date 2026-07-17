using UnityEngine;

public class ActiveGrenade : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject paintballPrefab;
    public int fragmentCount = 12;
    public float fragmentSpeed = 15f;
    public float autoExplodeTime = 5f;

    [HideInInspector] public WeaponSystem weaponSystem;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= autoExplodeTime)
            Explode();
    }

    public void Explode()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            Vector3 dir = Random.onUnitSphere;
            GameObject frag = Instantiate(paintballPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = frag.GetComponent<Rigidbody>();
            rb.linearVelocity = dir * fragmentSpeed;
        }

        if (weaponSystem != null)
            weaponSystem.ClearActiveGrenade();

        Destroy(gameObject);
    }
}
