using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;

    public GameObject deathPanel;

    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.onHit += UpdateHealth;
        playerHealth.onDie += Die;
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
}
