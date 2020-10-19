using FMODUnity;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musicEvent = "event:/Music/Music";
    [FMODUnity.EventRef]
    public string atmosphereEvent = "event:/Atmosphere/Atmosphere";
    [FMODUnity.EventRef]
    public string windEvent = "event:/Wind/Wind";
    [FMODUnity.EventRef]
    public string playerDamageEvent = "event:/Damage/PlayerDamage";
    [FMODUnity.EventRef]
    public string pickupEvent = "event:/Coin/Coin";
    
    private static FMOD.Studio.EventInstance musicInstance;
    private static FMOD.Studio.EventInstance atmosphereInstance;
    private static FMOD.Studio.EventInstance windInstance;

    private FMOD.Studio.PARAMETER_ID windParameterID;
    private FMOD.Studio.PARAMETER_ID playerDamageParameterID;

    public float damageFullVolume = 20.0f;
    
    private static SoundManager actualInstance;
    public static SoundManager instance
    {
        get
        {
            if (actualInstance != null)
                return actualInstance;
            actualInstance = FindObjectOfType<SoundManager>();
            if (actualInstance != null)
                return actualInstance;
            else
            {
                GameObject parent = new GameObject();
                actualInstance = parent.AddComponent<SoundManager>();
                parent.name = "SoundManager";
                return actualInstance;
            }
        }
    }

    private void Awake()
    {
        if (actualInstance != null && actualInstance != this)
        {
            Destroy(this);
        }
        else
        {
            actualInstance = this;
        }

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        atmosphereInstance = FMODUnity.RuntimeManager.CreateInstance(atmosphereEvent);
        windInstance = FMODUnity.RuntimeManager.CreateInstance(windEvent);

        FMOD.Studio.EventDescription windStrengthDescription;
        windInstance.getDescription(out windStrengthDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION windStrengthParameterDescription;
        windStrengthDescription.getParameterDescriptionByName("Wind Strength", out windStrengthParameterDescription);
        windParameterID = windStrengthParameterDescription.id;
        
        FMOD.Studio.EventDescription damageSoundDescription = FMODUnity.RuntimeManager.GetEventDescription(playerDamageEvent);
        FMOD.Studio.PARAMETER_DESCRIPTION damageSoundParameterDescription;
        damageSoundDescription.getParameterDescriptionByName("Damage Sound", out damageSoundParameterDescription);
        playerDamageParameterID = damageSoundParameterDescription.id;
    }

    private void Start()
    {
        musicInstance.start();
        atmosphereInstance.start();
        windInstance.start();
    }

    void Update()
    {
        UpdateWindVolume();
    }

    private void UpdateWindVolume()
    {
        float windStrength = WindManager.instance.currentAudioWindMagnitude;
        windInstance.setParameterByID(windParameterID, windStrength);
    }

    public void PlayPlayerDamage(float damageAmount)
    {
        var newInstance = FMODUnity.RuntimeManager.CreateInstance(playerDamageEvent);
        float damageIntensity = Mathf.Clamp01(damageAmount / damageFullVolume);
        newInstance.setParameterByID(playerDamageParameterID, damageIntensity);
        newInstance.start();
        newInstance.release();
    }

    public void PlayPickup()
    {
        var newInstance = FMODUnity.RuntimeManager.CreateInstance(pickupEvent);
        newInstance.start();
        newInstance.release();
    }

    private void OnDestroy()
    {
        atmosphereInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        windInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
