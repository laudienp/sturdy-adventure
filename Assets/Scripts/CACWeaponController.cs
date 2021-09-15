using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACWeaponController : MonoBehaviour
{
    public float attackRate = 0.5f;
    public float attackDamage = 50f;
    public float attackRange = 2f;

    public Animator anim;

    public GameObject swingSound;
    public GameObject bloodEffect;

    float lastAttack;

    private InputMaster input;

    private void Awake()
    {
        input = new InputMaster();
        input.Player.Fire.performed += ctx => Attack();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Attack()
    {
        if (lastAttack < Time.time)
        {
            Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(center, out RaycastHit hit, attackRange))
            {
                if (hit.collider.tag.Equals("Monster"))
                {
                    hit.collider.GetComponent<Damagable>().TakeDamage(attackDamage);
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
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
