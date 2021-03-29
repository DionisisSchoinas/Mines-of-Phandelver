using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        public List<ChannelData> channels;

        public SoundData()
        {
            channels = new List<ChannelData>();
        }

    }

    [System.Serializable]
    public class ChannelData
    {
        public string channel_name;
        public string param_name;
        public float value;

        public ChannelData(string channel_name, string param_name, float value)
        {
            this.channel_name = channel_name;
            this.param_name = param_name;
            this.value = value;
        }
    }

    public float volumeDbMin = -80f;
    public float volumeDbMax = 20f;

    private CanvasGroup canvasGroup;
    private Scrollbar master;
    private Scrollbar music;
    private Scrollbar effects;
    private Button closeButton;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        OverlayControls.SetCanvasState(false, canvasGroup);

        // Make sure range is smaller ot equal to [-80, 20]
        volumeDbMin = Mathf.Max(-80f, volumeDbMin);
        volumeDbMax = Mathf.Min(20f, volumeDbMax);

        Scrollbar[] scrollbars = gameObject.GetComponentsInChildren<Scrollbar>();
        master = scrollbars[0];
        music = scrollbars[1];
        effects = scrollbars[2];

        master.onValueChanged.AddListener(ChangeMaster);
        music.onValueChanged.AddListener(ChangeMusic);
        effects.onValueChanged.AddListener(ChangeEffects);

        closeButton = gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(CloseButtonClick);
    }

    private void Start()
    {
        LoadVolumeSettings();

        master.value = GetVolume("MasterVolume");
        music.value = GetVolume("MusicVolume");
        effects.value = GetVolume("EffectsVolume");
    }

    private void OnApplicationQuit()
    {
        SaveVolumeSettings();
    }

    private float GetVolume(string param)
    {
        float value;
        if (ResourceManager.Audio.AudioMixers.MainMixer.GetFloat(param, out value))
            return Mathf.InverseLerp(volumeDbMin, volumeDbMax, value);
        else
            return 0f;
    }

    private void SetVolume(string param, float value)
    {
        ResourceManager.Audio.AudioMixers.MainMixer.SetFloat(param, Mathf.Lerp(volumeDbMin, volumeDbMax, value));
        SaveVolumeSettings();
    }

    private void ChangeMaster(float value)
    {
        SetVolume("MasterVolume", value);
    }

    private void ChangeMusic(float value)
    {
        SetVolume("MusicVolume", value);
    }

    private void ChangeEffects(float value)
    {
        SetVolume("EffectsVolume", value);
    }

    private void SaveVolumeSettings()
    {
        SoundData soundData = new SoundData();
        soundData.channels.Add(new ChannelData("Master", "MasterVolume", master.value));
        soundData.channels.Add(new ChannelData("Music", "MusicVolume", music.value));
        soundData.channels.Add(new ChannelData("Effects", "EffectsVolume", effects.value));

        string data = JsonUtility.ToJson(soundData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SoundSettings.json", data);
    }

    private void LoadVolumeSettings()
    {
        try
        {
            string data = System.IO.File.ReadAllText(Application.persistentDataPath + "/SoundSettings.json");
            SoundData soundData = JsonUtility.FromJson<SoundData>(data);

            foreach (ChannelData channel in soundData.channels)
            {
                if (!ResourceManager.Audio.AudioMixers.MainMixer.SetFloat(channel.param_name, Mathf.Lerp(volumeDbMin, volumeDbMax, channel.value)))
                {
                    Debug.Log(channel.param_name + " not found");
                }
            }
        }
        catch
        {
            ResourceManager.Audio.AudioMixers.MainMixer.SetFloat("MasterVolume", 0f);
            ResourceManager.Audio.AudioMixers.MainMixer.SetFloat("MusicVolume", 0f);
            ResourceManager.Audio.AudioMixers.MainMixer.SetFloat("EffectsVolume", 0f);
        }

        Debug.Log("Loaded from : " + Application.persistentDataPath);
    }

    private void CloseButtonClick()
    {
        OverlayControls.SetCanvasState(false, canvasGroup);
    }
}
