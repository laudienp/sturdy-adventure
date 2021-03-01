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

    private Health health;
    private NavMeshAgent agent;
    private float waitTimer;

    private bool waiting;

    private Transform player;

    public float debug_angle;
    public float debug_dst;

    public bool visible;
    public float visibleDuration;

    public bool playerDetected;

    void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player").transform;

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
        }
        else if (waiting == false && Time.time > waitTimer && agent.remainingDistance <= agent.stoppingDistance)
        {
            waiting = true;
            waitTimer = Time.time + waitTime;
        }


        //////////////detection ///////
        Vector3 dir = player.position - transform.position;
        float dst = Vector3.Distance(transform.position, player.position);
        float angle = Vector3.Angle(transform.forward, dir);

        visible = false;

        if(dst < visionDistance && angle < visionAngle)
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

        if (visibleDuration > timeToDetectPlayer)
            playerDetected = true;


        if(playerDetected)
        {
            player.GetComponent<Health>().TakeDamage(100);
            playerDetected = false;
        }

    }

    void Die()
    {
        agent.isStopped = true;
        Destroy(gameObject);
    }
}
