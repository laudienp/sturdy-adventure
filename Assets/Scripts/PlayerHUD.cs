using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHUD : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;
    public Text checkpointText;

    public GameObject deathPanel;

    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.onHit += UpdateHealth;
        playerHealth.onDie += Die;
        playerHealth.onDie += UpdateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth()
    {
        healthText.text = "HP : " + playerHealth.health;
    }

    public void ResetHealth()
    {
        healthText.text = "HP : " + playerHealth.maxHealth;
    }

    void Die()
    {
        deathPanel.SetActive(true);
    }

    public void UpdateAmmo(int curr, int max)
    {
        ammoText.text = "Ammo : " + curr + " / " + max;
    }

    public void CheckpointPassed()
    {
        DOTween.Sequence()
            .Append(checkpointText.DOFade(1, 0.5f))
            .Append(checkpointText.DOFade(1, 2f))
            .Append(checkpointText.DOFade(0, 0.5f));
    }
}
