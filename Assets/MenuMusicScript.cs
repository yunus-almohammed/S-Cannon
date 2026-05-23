using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    AudioManager audioManager3;
    public string hg;
    public string mg;
    public void Music1()
    {
        audioManager3 = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager3.Play(hg);
        audioManager3.Stop(mg);
    }
}
