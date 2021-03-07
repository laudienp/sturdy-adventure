using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderTerritory : MonoBehaviour
{
    private Slender slender;

    private void Start()
    {
        slender = GameObject.FindGameObjectWithTag("Slender").GetComponent<Slender>();    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Slender"))
        {
            slender.RespawnSomewhereInTheForest();
        }
    }
}
