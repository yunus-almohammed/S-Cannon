using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desHiMas : MonoBehaviour
{
    private float destroy = 1f;
    void FixedUpdate()
    {
        Destroy(gameObject, destroy);
    }

}
