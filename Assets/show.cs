using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show : MonoBehaviour
{
    public static int sh;
    public GameObject g;

    void Update()
    {
        if (sh > 0)
        {
            g.SetActive(true);
            sh--;
        }
    }
}
