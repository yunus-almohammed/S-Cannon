using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 50;
    public Rigidbody2D rb;
    AudioManager audioManager;
    public string gf;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play(gf);
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        enmey enemy = hitInfo.GetComponent<enmey>();
        ENEMYS enemys = hitInfo.GetComponent<ENEMYS>();
        ENEMYB enemyb = hitInfo.GetComponent<ENEMYB>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (enemys != null)
        {
            enemys.TakeDamage(damage);
        }
       if (enemyb != null)
        {
            enemyb.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
