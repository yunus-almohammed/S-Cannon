using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public static int h = 100;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public static int num =0;
    public static int num2 =0;

    public Transform[] setpoints;

    void OnMouseDown()
    {
        
        if (ScoreScript.coin >= h && num < 5)
        {
            Debug.Log("down");
            ScoreScript.coin = ScoreScript.coin - h;
            Instantiate(enemyPrefabs[0], spawnPoints[num].position, spawnPoints[num].rotation);
            num++;
            if (num == 0)
            {
                h = 100;
            }
            else if (num == 1)
            {
                h = h + 20;
            }
            else if (num == 2)
            {
                h = h + 80;
            }
            else if (num == 3)
            {
                h = h + 100;
            }
            else if (num == 4)
            {
                h = h + 150;
            }
        }
    }

   
}

