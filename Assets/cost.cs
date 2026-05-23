using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cost : MonoBehaviour
{
    public TextMeshProUGUI costcan;

    // Update is called once per frame
    void Update()
    {
        if (Drag.num <= 4) { costcan.text = "Cost: " + Drag.h; }
        else
        {
            costcan.text = "Maximum";
        }
    }
}
