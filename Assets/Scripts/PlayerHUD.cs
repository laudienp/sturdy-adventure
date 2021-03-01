using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;

    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.onHit += UpdateHealth;
        playerHealth.onDie += ResetHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth()
    {
        healthText.text = "HP : " + playerHealth.health;
    }

    void ResetHealth()
    {
        healthText.text = "HP : " + playerHealth.maxHealth;
    }

    public void UpdateAmmo(int curr, int max)
    {
        ammoText.text = "Ammo : " + curr + " / " + max;
    }
}
