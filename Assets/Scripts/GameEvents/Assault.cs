using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : MonoBehaviour
{
    public GameObject mob;

    public Transform[] spawnpoints;

    public int waveSize = 1;

    public int waveCount = 1;


    private bool started;

    private int mobcount;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartAssault()
    {
        for(int c = 0; c < waveCount; c++)
        {
            mobcount = 0;

            for (int i = 0; i < waveSize; i++)
            {
                Transform spawnpoint = GetRandomSpawnpoint();
                GameObject g = Instantiate(mob, spawnpoint.position, spawnpoint.rotation);
                g.GetComponent<Health>().onDie += OnMobDie;

                yield return new WaitForSeconds(0.5f);
                mobcount++;
            }

            while (mobcount != 0)
                yield return new WaitForSeconds(1f);
        }
        
        //faire un truc pour dire que c'est fini

    }

    private Transform GetRandomSpawnpoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !started)
        {
            StartCoroutine("StartAssault");
            started = true;
        }
    }

    void OnMobDie()
    {
        mobcount--;
    }
}
