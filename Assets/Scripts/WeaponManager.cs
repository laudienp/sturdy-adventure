using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] weaponUnlocked;

    public float swapDelay = 0.5f;

    public int selectedWeapon;

    private float swapTimer;

    private void Start()
    {
        selectedWeapon = -1;
        SwapWeapon(0);
    }

    private void Update()
    {
        if(Time.time > swapTimer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapWeapon(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon(1);


            //with scrollwheel
            float scroll = Input.mouseScrollDelta.y;
            if(scroll > 0 && selectedWeapon < weapons.Length-1)
                SwapWeapon(selectedWeapon+1);
            else if(scroll < 0 && selectedWeapon > 0)
                SwapWeapon(selectedWeapon-1);
        }
    }

    private void SwapWeapon(int selection)
    {
        if (selectedWeapon == selection || !weaponUnlocked[selection]) // the weapon is already selected
            return;

        if(selectedWeapon > -1)
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
