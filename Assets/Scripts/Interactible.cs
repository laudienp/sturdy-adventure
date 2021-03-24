using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public string interactionMessage = "activer l'interrupteur";
    public string blockedMessage = "cette porte est bloquée";
    public bool blocked = false;
    public bool debug;
    
    public UnityEvent OnInteract;
    public UnityEvent OnInteractBlocked;

    private void Start()
    {
        if (debug)
            OnInteract.AddListener(InteractionDebug);
    }

    void InteractionDebug()
    {
        Debug.Log("Interact!!!");
    }

    public void BlockInteraction()
    {
        blocked = true;
    }

}
