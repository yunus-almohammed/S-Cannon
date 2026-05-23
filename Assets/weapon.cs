using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour {
   
    public Transform firepoint;
    public GameObject bulletPrefab;
    float timebetween;
    public float starttimebetween;

    void Start() 
    {
        timebetween = starttimebetween;

    }

    // Update is called once per frame
    void Update ()
    {
       
        if(timebetween <= 0)
        {
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            timebetween = starttimebetween;
        
        }
        else
        {
            timebetween -= Time.deltaTime;
        }
      
    }

    void shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}

