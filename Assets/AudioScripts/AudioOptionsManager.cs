using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }
    public static float MasterVolume { get; private set; }

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
    [SerializeField] private TextMeshProUGUI MasterSliderText;
    [SerializeField] private TextMeshProUGUI musicSliderText2;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText2;
    [SerializeField] private TextMeshProUGUI MasterSliderText2;
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;

        musicSliderText.text = ((int)(value * 100)).ToString();
        musicSliderText2.text = ((int)(value * 100)).ToString();
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        soundEffectsSliderText2.text = ((int)(value * 100)).ToString();
        AudioManager.Instance.UpdateMixerVolume();
    }
    public void OnMasterSliderValueChange(float value)
    {
        MasterVolume = value;

        MasterSliderText.text = ((int)(value * 100)).ToString();
        MasterSliderText2.text = ((int)(value * 100)).ToString();
        AudioManager.Instance.UpdateMixerVolume();
    }
}
