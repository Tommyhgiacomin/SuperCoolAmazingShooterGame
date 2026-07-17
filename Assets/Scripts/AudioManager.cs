using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [Header("Player Events")]
    public AK.Wwise.Event playerFire;
    
    public void PlayerFire(GameObject gameObject)
    {
        playerFire?.Post(gameObject);
    }

}
