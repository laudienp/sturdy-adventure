using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public string interactionMessage = "activer l'interrupteur";
    public bool debug;
    
    public UnityEvent OnInteract;

    private void Start()
    {
        if (debug)
            OnInteract.AddListener(InteractionDebug);
    }

    void InteractionDebug()
    {
        Debug.Log("Interact!!!");
    }

}
