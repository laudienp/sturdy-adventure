using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 5f;

    public Text interactionText;

    public Text blockedMessageText;

    private Interactible interactible;
    private bool interaction;
    private InputMaster input;

    private void Awake()
    {
        input = new InputMaster();
        input.Player.Interaction.performed += ctx => Interaction();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    void Update()
    {
        Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if(Physics.Raycast(center, out RaycastHit hit, interactionRange) && hit.transform.CompareTag("Interactible"))
        {
            interactible = hit.transform.GetComponent<Interactible>();
            interactionText.text = interactible.interactionMessage;
            interaction = true;
        }
        else
        {
            interactionText.text = "";
            interaction = false;
        }
    }

    private void Interaction()
    {
        if (!interaction)
            return;
        
        if (!interactible.blocked)
            interactible.OnInteract.Invoke();
        else
        {
            SayBlockedMessage(interactible.blockedMessage);
            interactible.OnInteractBlocked.Invoke();
        }
    }

    private void SayBlockedMessage(string msg)
    {
        blockedMessageText.text = msg;

        DOTween.Sequence()
            .Append(blockedMessageText.DOFade(1f, 0.5f))
            .Append(blockedMessageText.DOFade(1f, 2f))
            .Append(blockedMessageText.DOFade(0f, 0.5f));
    }
}
