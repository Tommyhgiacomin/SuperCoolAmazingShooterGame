using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Music_Callback : MonoBehaviour
{
    public AK.Wwise.Event musicEvent;

    public Light beatLight;

    public float pulseIntensity = 5f;
    public float fadeSpeed = 8f;

    private float beatLightTarget = 0f;
    private Color colorTarget = Color.white;

    public Material skyBoxDay;
    public Material skyBoxNight;

    private void Start()
    {
        musicEvent.Post(gameObject,
            (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncUserCue),
            OnMusicCallback
            );
    }

    void OnMusicCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            beatLightTarget = pulseIntensity;

            Debug.Log("OtherWwise (hehe)");
        }

        if (in_type == AkCallbackType.AK_MusicSyncUserCue)
        {
            AkMusicSyncCallbackInfo info = (AkMusicSyncCallbackInfo)in_info;
            string cueName = info.userCueName;

            switch (cueName)
            {
                case "Change_Color_Red":
                    colorTarget = Color.red;
                    RenderSettings.skybox = skyBoxNight;
                    DynamicGI.UpdateEnvironment();
                    break;
                case "Change_Color_Green":
                    colorTarget = Color.green;
                    break;
                case "Change_Color_White":
                    colorTarget = Color.white;
                    RenderSettings.skybox = skyBoxDay;
                    DynamicGI.UpdateEnvironment();
                    break;
            }
        }

    }

    private void Update()
    {
        beatLight.intensity = Mathf.Lerp( beatLight.intensity, beatLightTarget, fadeSpeed * Time.deltaTime);
        beatLightTarget = Mathf.Lerp(beatLightTarget, 0f, fadeSpeed * Time.deltaTime);

        beatLight.color = colorTarget;
    }

}
