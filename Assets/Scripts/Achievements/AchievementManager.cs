using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject panel;
    public Text achievementText;

    public void UnlockAchievement(string achievement) // add persistency
    {
        achievementText.text = achievement;
        panel.SetActive(true);

        Invoke("HidePanel", 5);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
    }
}
