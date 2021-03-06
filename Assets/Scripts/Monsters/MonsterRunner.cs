using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterRunner : MonoBehaviour
{
    public float damage = 10;
    public float attackSpeed = 1;

    public float whilhemScreamChance = 0.1f;

    public AudioClip deathSound;
    public AudioClip[] hitSounds;
    public AudioClip[] screamSounds;
    public Vector2 screamDelay;
    public bool screamOnSpawn;

    public GameObject hitbox;

    public bool chasePlayer = true;

    public GameObject whilhemScreamSound;

    private GameObject player;

    private NavMeshAgent agent;

    private float nextAttack;
    private float screamTimer;

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

        screamTimer = Time.time + Random.Range(screamDelay.x, screamDelay.y);

        if (screamOnSpawn)
            screamTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(chasePlayer)
            agent.SetDestination(player.transform.position);

        animator.SetFloat("Speed", 1f);

        if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance + 0.5f
            && nextAttack < Time.time)
        {
            animator.SetTrigger("Attack");
            player.GetComponent<Health>().TakeDamage(damage);

            nextAttack = Time.time + attackSpeed;
        }

        //scream

        if(Time.time > screamTimer)
        {
            int index = Random.Range(0, screamSounds.Length);

            AudioSource.PlayClipAtPoint(screamSounds[index], transform.position, 1);

            screamTimer = Time.time + Random.Range(screamDelay.x, screamDelay.y);
        }
    }

    void Die()
    {
        if(Random.value < whilhemScreamChance)
        {
            GameObject s = Instantiate(whilhemScreamSound, transform.position, Quaternion.identity);
            Destroy(s, 5);
        }
        else
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
        }
        
        animator.SetTrigger("Die");
        hitbox.SetActive(false);
        agent.isStopped = true;
        enabled = false;
        Destroy(gameObject, 10);
    }

    void Hit()
    {
        int index = Random.Range(0, hitSounds.Length);

        AudioSource.PlayClipAtPoint(hitSounds[index], transform.position, 1f);
    }
}
