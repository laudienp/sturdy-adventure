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

    private InputMaster input;

    private void Awake()
    {
        input = new InputMaster();

        input.Player.Weapon1.performed += ctx => SwapWeapon(0);
        input.Player.Weapon2.performed += ctx => SwapWeapon(1);
        input.Player.ScrollWeapon.performed += ctx => ScrollWeapon();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    private void Start()
    {
        selectedWeapon = -1;
        SwapWeapon(0);
    }

    private void SwapWeapon(int selection)
    {
        if (Time.time < swapTimer) // not ready to swap
            return;

        if (selectedWeapon == selection || !weaponUnlocked[selection]) // the weapon is already selected
            return;

        if(selectedWeapon > -1)
            weapons[selectedWeapon].SetActive(false);
        selectedWeapon = selection;
        weapons[selectedWeapon].SetActive(true);

        swapTimer = Time.time + swapDelay;
    }

    private void ScrollWeapon()
    {
        float scroll = input.Player.ScrollWeapon.ReadValue<float>();

        if (scroll > 0 && selectedWeapon < weapons.Length - 1)
            SwapWeapon(selectedWeapon + 1);
        else if (scroll < 0 && selectedWeapon > 0)
            SwapWeapon(selectedWeapon - 1);
    }

    public void TakeAmmo(int count, int weaponIndex)
    {
        weapons[weaponIndex].GetComponent<WeaponController>().Ammo(count);
    }

    public void UnlockWeapon(int weaponIndex)
    {
        weaponUnlocked[weaponIndex] = true;
    }
}
