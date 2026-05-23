using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour
{
    AudioManager audioManager2;
    public string fg;
    public string bg;
    // Start is called before the first frame update
    void Start()
    {
        audioManager2 = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager2.Play(fg);
        audioManager2.Stop(bg);
    }
}
