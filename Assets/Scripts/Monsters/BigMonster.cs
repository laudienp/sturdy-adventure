using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigMonster : MonoBehaviour
{
    public float damage = 10;
    public float attackSpeed = 1;

    public GameObject explosion;
    public GameObject blood;

    public Transform[] checkpoints;

    private GameObject player;

    private NavMeshAgent agent;

    private float nextAttack;

    private Health health;

    private Animator animator;

    private int compteur;


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
        compteur = 0;
        agent.SetDestination(checkpoints[compteur].position);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", 0.5f);
        if (Vector3.Distance(transform.position, player.transform.position) < 5.0)
            agent.SetDestination(player.transform.position);
        else if(Vector3.Distance(transform.position, checkpoints[compteur].position) < 5.0)
        {
            compteur++;
            if (compteur == 5) compteur = 0;
            agent.SetDestination(checkpoints[compteur].position);
        }
        else
        {
            agent.SetDestination(checkpoints[compteur].position);
        }
        

        if (Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance + 0.5f
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
        Destroy(gameObject);
    }

    void Hit()
    {
        Instantiate(blood, transform.position + Vector3.up, Quaternion.identity);
    }
}
