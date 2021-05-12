using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Text objectifText;
    public Image fadePanel;
    public GameObject quitGame;
    
    public Interactible libDoor;
    public Interactible basementDoor;
    public Interactible car;



    public string objectif1 = "Vérifier l'interieur du batiment";
    public string objectif2 = "Allez chercher les différentes parties du code pour ouvrir la porte dans le jardin";
    public string objectif3 = "Allez dans la bibliothèque";
    public string objectif4 = "Eliminez les espions";
    public string objectif5 = "Activez les deux générateurs du sous-sols";
    public string objectif6 = "Retournez a votre voiture";

    public bool objectif1Validated = false;
    public bool objectif3Validated = false;
    public int paperCount = 0;

    public int guardDeadCount = 0;
    public int activatedEngine = 0;

    public GameObject pauseMenu;
    public PlayerController pcontroller;

    public void Start()
    {
        SayObjectif(objectif1);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pcontroller.IsDead())
        {
            if (pauseMenu.activeInHierarchy)
                CloseMenu();
            else
                OpenMenu();
        }
    }

    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pcontroller.freezeControl = false;
    }

    public void OpenMenu()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pcontroller.freezeControl = true;
    }

    public void CheckLibDoor()
    {
        if(!objectif1Validated)
        {
            objectif1Validated = true;

            SayObjectif(objectif2);
        }
    }

    public void TakePaper()
    {
        paperCount++;

        if (paperCount == 4)
        {
            libDoor.blocked = false;
            SayObjectif(objectif3);
        }
    }

    public void GuardKilled()
    {
        guardDeadCount++;

        if(guardDeadCount == 6)
        {
            basementDoor.blocked = false;
            SayObjectif(objectif5);
        }
    }

    public void OnActivatedEngine()
    {
        activatedEngine++;

        if(activatedEngine == 2)
        {
            SayObjectif(objectif6);
            car.blocked = false;
        }
    }

    public void OnEnterCar()
    {
        EndGame();
    }

    public void EnterInLib()
    {
        if(!objectif3Validated)
        {
            objectif3Validated = true;
            SayObjectif(objectif4);
        }
    }

    public void SayObjectif(string text)
    {
        objectifText.text = "Objectif : " + text;

        DOTween.Sequence()
            .Append(objectifText.DOFade(1f, 1f))
            .Append(objectifText.DOFade(1f, 5f))
            .Append(objectifText.DOFade(0f, 1f));
    }

    public void EndGame()
    {
        objectifText.text = "GG !";
        DOTween.Sequence()
            .Append(fadePanel.DOFade(1f, 1f))
            .Append(objectifText.DOFade(1f, 1f).OnComplete(DisplayQuitButton));
    }

    public void DisplayQuitButton()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        quitGame.SetActive(true);
    }

    public void SetQwerty()
    {
        pcontroller.isQwerty = true;
    }

    public void SetAzerty()
    {
        pcontroller.isQwerty = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
