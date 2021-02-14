using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public string interactionMessage = "activer l'interrupteur";
    public bool debug;

    public UnityAction OnInteract;

    private void Start()
    {
        if (debug)
            OnInteract += InteractionDebug;
    }

    void InteractionDebug()
    {
        Debug.Log("Interact!!!");
    }

}
