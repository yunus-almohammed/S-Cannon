using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MUSIC"))
        {
            PlayerPrefs.SetFloat("MUSIC", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    // Update is called once per frame
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MUSIC");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("MUSIC", volumeSlider.value);
    }
}
