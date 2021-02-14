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



    public void StartAssault()
    {
        if (started)
            return;

        StartCoroutine("AssaultRoutine");
        started = true;
    }

    public IEnumerator AssaultRoutine()
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

    void OnMobDie()
    {
        mobcount--;
    }
}
