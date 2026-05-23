using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ENEMYS : MonoBehaviour
{
    public float health1;
    public float maxHealth1 = 180;
    public healthBar Healthbar;
    public GameObject damageText;

    public GameObject deathEffect;

    void Start()
    {
        health1 = maxHealth1;
        Healthbar.SetHealth(health1, maxHealth1);
    }

    void Awake()
    {
        maxHealth1 = 180 + RandomSpawn.helincl;
    }

    public void TakeDamage(int damage)
    {
        health1 -= damage;
        Healthbar.SetHealth(health1, maxHealth1);

        if (health1 <= 0)
        {
            Die1();
        }
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
    void Die1()
    {
        ScoreScript.scoreValue += 35;
        ScoreScript.coin += 15;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
