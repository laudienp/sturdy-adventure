using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{

    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();

        if (!health)
            health = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}
