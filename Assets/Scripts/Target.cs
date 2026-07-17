using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float lifetime = 8f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void DestroyTarget()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(scoreValue);

        Destroy(gameObject);
    }
}
