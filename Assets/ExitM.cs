using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ExitM : MonoBehaviour
{
    public static int Emerald = 0;
    public TextMeshProUGUI tx;
    public GameObject em;
    AudioManager audioManager3;
    public string hg;
    public string mg;
    public Button ad;
    public void Resumekj()
    {

        if (Emerald > 0)
        {
            Time.timeScale = 1f;
            em.SetActive(false);
            Emerald--;
        }
        else if (Emerald <= 0)
        {
            tx.text = "You Have to watch an add ";
        }
        Debug.Log(Emerald);
    }
    public void Quittt()
    {

        audioManager3 = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager3.Play(hg);
        audioManager3.Stop(mg);
        Time.timeScale = 1f;
        ScoreScript.coin = 100;
        ScoreScript.scoreValue = 0;
        Drag.h = 100;
        Drag.num = 0;
        RandomSpawn.helincl = 0;
        RandomRespawnBoss.mHI = 0;
        RandomRespawnBoss.timebetween = 40f;
        ad.interactable = true;
    }

    public void ReStartt()
    {
        Time.timeScale = 1f;
        ScoreScript.coin = 100;
        ScoreScript.scoreValue = 0;
        Drag.h = 100;
        Drag.num = 0;
        RandomSpawn.helincl = 0;
        RandomRespawnBoss.mHI = 0;
        RandomRespawnBoss.timebetween = 40f;
        ad.interactable = true;
    }

    public void DestroyT(string tag)
    {
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in gameobjects)
        {
            GameObject.Destroy(target); 
        }
    }

    public void Resum()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Emerald > 0)
        {
            tx.text = "You can Resume ";
        }
        else if (Emerald <= 0)
        {
            tx.text = "You Have to watch an add to Resume";
        }

        Debug.Log(Emerald);
    }
}
