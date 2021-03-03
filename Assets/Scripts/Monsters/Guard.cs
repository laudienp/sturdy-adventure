using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class Guard : MonoBehaviour
{
    public Transform[] checkpoints;
    public float waitTime = 5f;

    public float visionAngle = 50f;
    public float visionDistance = 10f;
    public float timeToDetectPlayer = 2f;

    public GameObject shotSound;


    private Health health;
    private NavMeshAgent agent;
    private Animator anim;
    private float waitTimer;

    private bool waiting;

    private Transform player;
    private Health playerHealth;

    public float debug_angle;
    public float debug_dst;

    public bool visible;
    public float visibleDuration;

    public bool playerDetected;

    public float currentVelocity;
    private Vector3 prevPos;

    private float shootTimer;

    void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();

        //waitTimer = Time.time + waitTime;
        waiting = true;

        health.onDie += Die;
    }

    // Update is called once per frame
    void Update()
    {
        /////////////// movement routine /////////////////
        if (waiting == true && Time.time > waitTimer)
        {
            int next = Random.Range(0, checkpoints.Length);
            agent.SetDestination(checkpoints[next].position);

            waitTimer = Time.time + waitTime;
            waiting = false;
            //anim.SetFloat("Speed", 1f);
        }
        else if (waiting == false && Time.time > waitTimer && agent.remainingDistance <= agent.stoppingDistance)
        {
            waiting = true;
            waitTimer = Time.time + waitTime;

            //anim.SetFloat("Speed", 0f);
        }


        //////////////detection ///////
        Vector3 dir = player.position - transform.position;
        float dst = Vector3.Distance(transform.position, player.position);
        float angle = Vector3.Angle(transform.forward, dir);

        visible = false;

        if(dst < visionDistance && angle < visionAngle && !playerHealth.isDead)
        {
            //// obstruction detection
            Ray r = new Ray(transform.position, dir);
            RaycastHit[] hits = Physics.RaycastAll(r, dst);

            visible = true;
            foreach(RaycastHit hit in hits)
            {
                if (!(hit.transform.root.CompareTag("Player") || hit.transform.root.CompareTag("Monster")))
                    visible = false;
            }
        }

        if (visible)
            visibleDuration += Time.deltaTime;
        else
            visibleDuration = 0;

        if (!playerDetected && visibleDuration > timeToDetectPlayer)
        {
            playerDetected = true;
            anim.SetBool("PlayerDetected", true);
            shootTimer = Time.time + 1f;

            agent.isStopped = true;
        }
            


        if(playerDetected && Time.time > shootTimer)
        {
            anim.SetTrigger("Shoot");

            GameObject s = Instantiate(shotSound, transform.position, Quaternion.identity);
            Destroy(s, 5);

            shootTimer = Time.time + 1f;
            player.GetComponent<Health>().TakeDamage(100); //one shot for the moment
            playerDetected = false;
            anim.SetBool("PlayerDetected", false);
            agent.isStopped = false;
        }

        Vector3 currentMove = transform.position - prevPos;
        currentVelocity = currentMove.magnitude / Time.deltaTime;
        prevPos = transform.position;

        anim.SetFloat("Speed", currentVelocity);
    }

    void Die()
    {
        agent.isStopped = true;
        Destroy(gameObject);
    }
}
