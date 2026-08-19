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

    [Header("Wwise Ambience Events")]
    public AK.Wwise.Event ambienceEvent;

    [Header("Wwise NPC Events")]
    public AK.Wwise.Event targetSpawnEvent;

    [Header("Wwise Collectible Events")]
    public AK.Wwise.Event collectiblePickupEventGrenadeLauncher;
    public AK.Wwise.Event collectiblePickupEventSniper;
    public AK.Wwise.Event collectiblePickupEventShotgun;
    
    [Header("Wwise Music Events")]
    public AK.Wwise.Event musicEvent;
    public AK.Wwise.Event musicEventStop;

    [Header("Wwise Weapon Events")]
    public AK.Wwise.Event sniperHitEvent;
    public AK.Wwise.Event targetHitEventJaguar;
    public AK.Wwise.Event targetHitEventMonkey;
    public AK.Wwise.Event targetHitEventCrocodile;
    public AK.Wwise.Event grenadeExplosionEvent;
    public AK.Wwise.Event paintballImpactEvent;
    public AK.Wwise.Event weaponFireEventGrenadeLauncher;
    public AK.Wwise.Event weaponFirePickupEventSniper;
    public AK.Wwise.Event weaponFirePickupEventShotgun;
    public AK.Wwise.Event weaponFirePickupEventPaintball;

    [Header("Wwise UI Events")]
    public AK.Wwise.Event uiClickEvent;

    [Header("Wwise States")]
    public AK.Wwise.State stateGameplay;
    public AK.Wwise.State stateMenu;

    [Header("Wwise RTPCs")]
    public AK.Wwise.RTPC rtpcPlayerSpeed;
    public AK.Wwise.RTPC rtpcScore;

    public void PlayAmbience(GameObject gameObject)
    {
        ambienceEvent?.Post(gameObject);
    }

    public void GameState(int state)
    {
        if (state == 1)
        {
            stateGameplay.SetValue(gameObject);
        }

        if(state == 0)
        {
            stateMenu.SetValue(gameObject);
        }
    }

    //AudioManager.Instance.switchname.SetValue(gameObject);

}
