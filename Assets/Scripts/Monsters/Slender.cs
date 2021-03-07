using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slender : MonoBehaviour
{
    public float detectionRange;
    public float attackRange;
    public float attackDelay;
    public float runSpeed;
    public float walkSpeed;
    public float jumpSpeed;
    public float jumpHitRange;

    public float chasingDuration;
    public float hidingDuration;

    public GameObject[] footstepSounds;
    public Transform[] spawnPoints;



    Animator anim;
    NavMeshAgent agent;
    Health playerHealth;
    Transform player;
    SkinnedMeshRenderer model;

    bool chase;
    bool hiding;

    float attackTimer;
    float chasingTimer;
    float hidingTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();
        model = GetComponentInChildren<SkinnedMeshRenderer>();

        chase = false;
    }

    void Update()
    {

        if (hiding && hidingTimer > Time.time)
            return;
        if(hiding && hidingTimer < Time.time)
        {
            RespawnSomewhereInTheForest();
            hiding = false;
            model.enabled = true;
        }

        if(Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            chase = true;
            anim.SetFloat("Speed", 1);
            agent.speed = runSpeed;
            agent.isStopped = false;
            chasingTimer = Time.time + chasingDuration;
        }
        


        if (chase && attackTimer < Time.time)
        {
            agent.SetDestination(player.position);


            if (Vector3.Distance(transform.position, player.position) < attackRange && attackTimer < Time.time)
            {
                anim.SetTrigger("Attack");
                agent.speed = jumpSpeed;
                anim.SetFloat("Speed", 0);
                attackTimer = Time.time + attackDelay;
            }
            else if(chasingTimer < Time.time)
            {
                chase = false;
                anim.SetFloat("Speed", 0);
                hidingTimer = hidingDuration + Time.time;
                hiding = true;
                agent.isStopped = true;
                model.enabled = false;
            }
            else
            {
                anim.SetFloat("Speed", 1);
                agent.speed = runSpeed;
            }

        }
    }

    public void RespawnSomewhereInTheForest()
    {
        agent.isStopped = true;
        anim.SetFloat("Speed", 0);
        int index = Random.Range(0, spawnPoints.Length);

        Teleport(spawnPoints[index]);
    }

    void OnAnimatorIK()
    {

        anim.SetLookAtWeight(1f, 0.2f, 0.8f, 0.9f);
        anim.SetLookAtPosition(player.position);
    }

    public void AttackAnimationEvent()
    {
        float dst = Vector3.Distance(transform.position, playerHealth.transform.position);

        if (dst < jumpHitRange)
            playerHealth.TakeDamage(100);
    }

    public void PlayFootStepSound()
    {
        int index = Random.Range(0, footstepSounds.Length);

        GameObject g = Instantiate(footstepSounds[index], transform.position, Quaternion.identity);
        Destroy(g, 2);
    }

    private void Teleport(Transform point)
    {
        transform.position = point.position;
    }
}
