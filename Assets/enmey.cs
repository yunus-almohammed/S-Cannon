using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class enmey : MonoBehaviour {
    public GameObject damageText;
    public GameObject deathEffect;
    public healthBar Healthbar;

    public float health1;
    public float maxHealth1 = 180;

   void Start ()
    {
        health1 = maxHealth1;
        Healthbar.SetHealth(health1, maxHealth1);
    }

    void Awake()
    {
        maxHealth1 = 180 + RandomSpawn.helincl;
    } 
    public void TakeDamage (int damage)
    {
        health1 -= damage;
        Healthbar.SetHealth(health1, maxHealth1);

        if (health1 <= 0)
        {
            Die();
        }
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
    void Die()
    {            
        ScoreScript.scoreValue += 25;
        ScoreScript.coin += 10;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);      
    }



    
}
