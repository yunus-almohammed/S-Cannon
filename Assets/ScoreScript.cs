using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static int coin = 100;
    public static int scoreValue = 0;     
    public TextMeshProUGUI score;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI tes;

    // Start is called before the first frame update

    void Start()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = GetComponent<TextMeshProUGUI>();      
        score.text = "Score:" + scoreValue;

        Coin.text = "Coin:" + coin;

        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            HighScore.text = "HighScore\n" + scoreValue;
        }
    }


}
