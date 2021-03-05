﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACWeaponController : MonoBehaviour
{
    public float attackRate = 0.5f;
    public float attackDamage = 50f;
    public float attackRange = 2f;

    public Animator anim;

    public GameObject swingSound;

    float lastAttack;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && lastAttack < Time.time)
        {
            Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if(Physics.Raycast(center, out RaycastHit hit, attackRange))
            {
                if (hit.collider.tag.Equals("Monster"))
                    hit.collider.GetComponent<Damagable>().TakeDamage(attackDamage);

                
            }

            //anim
            anim.SetTrigger("Attack");
            //sound
            GameObject g = Instantiate(swingSound, transform.position, Quaternion.identity);
            Destroy(g, 2);

            lastAttack = Time.time + attackRate;
        }
    }
}
