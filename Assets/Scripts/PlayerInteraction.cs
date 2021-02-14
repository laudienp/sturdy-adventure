using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 5f;

    public Text interactionText;

    void Update()
    {
        Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if(Physics.Raycast(center, out RaycastHit hit, interactionRange) && hit.transform.CompareTag("Interactible"))
        {
            Interactible interactible = hit.transform.GetComponent<Interactible>();
            interactionText.text = interactible.interactionMessage;

            if(Input.GetKeyDown(KeyCode.E))
                interactible.OnInteract.Invoke();
        }
        else
        {
            interactionText.text = "";
        }

        
    }
}
