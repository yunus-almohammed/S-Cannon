using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resethi : MonoBehaviour
{
    public GameObject massage;
    public void Reset()
    {
        GameObject enemy = Instantiate(massage, new Vector3(0, -330, 0), Quaternion.identity) as GameObject;
        enemy.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        PlayerPrefs.DeleteAll();
    }
}


