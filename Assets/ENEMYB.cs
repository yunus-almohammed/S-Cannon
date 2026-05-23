using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ENEMYB : MonoBehaviour
{

    public float health1;
    public float maxHealth1 = 1000;
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
        maxHealth1 = 1000 + RandomRespawnBoss.mHI;
    }

    public void TakeDamage(int damage)
    {
        health1 -= damage;
        Healthbar.SetHealth(health1, maxHealth1);

        if (health1 <= 0)
        {
            Die2();
        }
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
    void Die2()
    {
        ScoreScript.scoreValue += 100;
        ScoreScript.coin += 40;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
