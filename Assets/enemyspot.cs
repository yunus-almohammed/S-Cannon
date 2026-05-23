using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspot : MonoBehaviour
{
    public float speed = 20f;
   // public static int Dmoney;//to make the maine money == scoreValue/25
    public Rigidbody2D rb;
    public GameObject me;
    RewardedAdExample ad;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        ad = GameObject.FindGameObjectWithTag("AdManager").GetComponent<RewardedAdExample>();
    }

    void FixedUpdate()
    {
        if (rb.position.y < -7.05)
        {
           // Dmoney = ScoreScript.scoreValue/25;
            money.Amoney += ScoreScript.scoreValue / 25;
            FindObjectOfType<GameManager>().EndGame();
            Time.timeScale = 0f;
            show.sh++;
            ad.ShowAd2();
            Destroy(me);
        }
    }
}
