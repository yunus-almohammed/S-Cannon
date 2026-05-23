using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class money : MonoBehaviour
{
    public static int Amoney = 0;
    public TextMeshProUGUI Tmoney;

    void Start()
    {
        
        Tmoney.text = PlayerPrefs.GetInt(" ", Amoney).ToString();
    }

    void Update()
    {
        //Tmoney = GetComponent<TextMeshProUGUI>();
        

        //Tmoney.text = " " + Amoney;
        PlayerPrefs.SetInt(" ", Amoney);
        Tmoney.text = " " + Amoney;
    }

}
