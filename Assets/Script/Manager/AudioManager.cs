using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : PersistentSingleton<AudioManager> {

    [SerializeField]
    private GameSetting gameSetting;
    [SerializeField]
    private AudioMixer mixer;

    private float musicValume;
    private float mainLastValume;
    private bool silent;

    protected override void Awake()
    {
        base.Awake();

        musicValume = gameSetting.musicValume;
        mainLastValume = gameSetting.mainLastValume;
        mixer.SetFloat("music", musicValume);
        mixer.SetFloat("main", mainLastValume);
    }

    public float MusicValume {
        get { return musicValume; }
        set
        {
            gameSetting.musicValume = value;
            musicValume = value;
            mixer.SetFloat("music", value);
        }
    }

    public float Silent
    {
        get { return mainLastValume; }
        set
        {
            gameSetting.mainLastValume = value;
            mainLastValume = value;
            mixer.SetFloat("main", value);
        }
    }
}
