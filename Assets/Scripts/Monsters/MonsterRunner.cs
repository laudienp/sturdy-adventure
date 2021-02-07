using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterRunner : MonoBehaviour
{
    public float damage = 10;
    public float attackSpeed = 1;

    public GameObject explosion;
    public GameObject blood;

    public GameObject hitbox;

    private GameObject player;

    private NavMeshAgent agent;

    private float nextAttack;

    private Health health;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();

        animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        health.onDie += Die;
        health.onHit += Hit;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        animator.SetFloat("Speed", 1f);

        if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance + 0.5f
            && nextAttack < Time.time)
        {
            animator.SetTrigger("Attack");
            player.GetComponent<Health>().TakeDamage(damage);

            nextAttack = Time.time + attackSpeed;
        }
    }

    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        animator.SetTrigger("Die");
        hitbox.SetActive(false);
        agent.isStopped = true;
        enabled = false;
        //Destroy(gameObject);
    }

    void Hit()
    {
        Instantiate(blood, transform.position +Vector3.up, Quaternion.identity);
    }
}
