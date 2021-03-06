using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainZone : MonoBehaviour
{
    public FollowPlayer rain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            rain.follow = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            rain.follow = false;
    }
}
