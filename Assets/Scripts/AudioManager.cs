using System;
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

    [Header("Wwise Player Events")]
    public AK.Wwise.Event weaponFireEvent;

    [Header("Wwise Ambience Events")]
    public AK.Wwise.Event ambienceEvent;

    [Header("Wwise NPC Events")]
    public AK.Wwise.Event targetSpawnEvent;

    [Header("Wwise Collectible Events")]
    public AK.Wwise.Event collectiblePickupEvent;

    [Header("Wwise Music Events")]
    public AK.Wwise.Event musicEvent;

    [Header("Wwise Weapon Events")]
    public AK.Wwise.Event sniperHitEvent;
    public AK.Wwise.Event targetHitEvent;
    public AK.Wwise.Event grenadeExplosionEvent;
    public AK.Wwise.Event paintballImpactEvent;

    [Header("Wwise UI Events")]
    public AK.Wwise.Event uiClickEvent;
    public AK.Wwise.Event stateMenu;

    [Header("Wwise Switches")]
    public AK.Wwise.Switch weaponSwitch;
    public AK.Wwise.Switch grenadeSwitch;
    public AK.Wwise.Switch surfaceMaterial;

    [Header("Wwise RTPCs")]
    public AK.Wwise.RTPC rtpcplayerSpeed;
    public AK.Wwise.RTPC rtpcScore;

    public void PlayerFire(GameObject gameObject)
    {
        weaponFireEvent?.Post(gameObject);
    }

    public void PlayAmbience(GameObject gameObject)
    {
        ambienceEvent?.Post(gameObject);
    }

    //        AudioManager.Instance.switchname.SetValue(gameObject);

}
