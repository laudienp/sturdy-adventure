﻿using System.Collections;
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

    public float chasingDuration;
    public float hidingDuration;

    Animator anim;
    NavMeshAgent agent;
    Health player;

    bool chase;

    Transform target;

    float attackTimer;
    float chasingTimer;
    float hidingTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        chase = false;
    }

    void Update()
    {

        if (hidingTimer > Time.time)
            return;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectionRange, transform.forward);

        foreach (RaycastHit hit in hits)
        {
            if (target == null && hit.transform.CompareTag("Player"))
            {
                chase = true;
                target = hit.transform;
                anim.SetFloat("Speed", 1);
                agent.speed = runSpeed;
                agent.isStopped = false;
                chasingTimer = Time.time + chasingDuration;
            }
        }
        


        if (chase && attackTimer < Time.time)
        {
            agent.SetDestination(target.position);


            if (Vector3.Distance(transform.position, target.position) < attackRange && attackTimer < Time.time)
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
                target = null;
                agent.isStopped = true;
            }
            else
            {
                anim.SetFloat("Speed", 1);
                agent.speed = runSpeed;
            }

        }
    }

    void OnAnimatorIK()
    {

        anim.SetLookAtWeight(1f, 0.2f, 0.8f, 0.9f);
        if (target != null)
            anim.SetLookAtPosition(target.position);
    }

    public void AttackAnimationEvent()
    {
        float dst = Vector3.Distance(transform.position, player.transform.position);

        if (dst < 2f)
            player.TakeDamage(100);
    }
}
