using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;

    public float health;
    public bool isDead;

    public UnityAction onDie;
    public UnityAction onHit;



    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            isDead = true;
            health = 0;

            if(onDie != null)
                onDie.Invoke();
        }
        else
        {
            if (onHit != null)
                onHit.Invoke();
        }
            
    }

    public void Full()
    {
        health = maxHealth;
        isDead = false;
    }
}
