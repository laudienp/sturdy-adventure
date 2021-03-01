using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public float swapDelay = 0.5f;

    public int selectedWeapon;

    private float swapTimer;

    private void Start()
    {
        selectedWeapon = 0;
    }

    private void Update()
    {
        if(Time.time > swapTimer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapWeapon(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon(1);
        }
    }

    private void SwapWeapon(int selection)
    {
        if (selectedWeapon == selection) // the weapon is already selected
            return;

        weapons[selectedWeapon].SetActive(false);
        selectedWeapon = selection;
        weapons[selectedWeapon].SetActive(true);

        swapTimer = Time.time + swapDelay;
    }

    public void TakeAmmo(int count, int weaponIndex)
    {
        weapons[weaponIndex].GetComponent<WeaponController>().Ammo(count);
    }
}
