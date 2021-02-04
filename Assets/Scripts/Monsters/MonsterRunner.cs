using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterRunner : MonoBehaviour
{
    public float damage = 10;
    public float attackSpeed = 1;

    public GameObject explosion;

    private GameObject player;

    private NavMeshAgent agent;

    private float nextAttack;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();

        health.onDie += Die;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance + 0.5f
            && nextAttack < Time.time)
        {
            player.GetComponent<Health>().TakeDamage(damage);

            nextAttack = Time.time + attackSpeed;
        }
    }

    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
