using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour {

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Toggle silent;

    private float currentValume;

	void Start ()
    {
        currentValume = AudioManager.Instance.Silent;
        musicSlider.value = AudioManager.Instance.MusicValume;
    }

    public void OnMusicSilder(float valume)
    {
        AudioManager.Instance.MusicValume = valume;
    }
	public void OnSilentBool(bool silent)
    {
        if (!silent)
        {
            AudioManager.Instance.Silent = -80;
        }
        else
        {
            AudioManager.Instance.Silent = currentValume;
        }
    }
}
