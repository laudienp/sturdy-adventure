using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;

    public float health;

    public UnityAction onDie;
    public UnityAction onHit;



    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (onHit != null)
            onHit.Invoke();

        if (health <= 0)
        {
            if(onDie != null)
                onDie.Invoke();
        }
            
    }

    public void Full()
    {
        health = maxHealth;
    }
}
