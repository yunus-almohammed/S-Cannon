using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject[] levels;
    int  current_level = 0;

   


    public void upgrade()
    {
        if(current_level < levels.Length - 1)
        {
            current_level++;
            Up(current_level); 
        }
    }

    void Up(int LVL)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == LVL)
                levels[i].SetActive(true);
            else
                levels[i].SetActive(false);

        }
    }
}
