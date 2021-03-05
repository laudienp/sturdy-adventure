using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZone : MonoBehaviour
{
    private AudioSource ambientSound;

    private void Start()
    {
        ambientSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            ambientSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            ambientSound.Stop();
    }
}
