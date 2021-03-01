using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo = 10;
    public int weaponIndex = 1;
    public GameObject ammoSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            other.transform.root.GetComponent<WeaponManager>().TakeAmmo(ammo, weaponIndex);

            GameObject g = Instantiate(ammoSound, transform.position, Quaternion.identity);
            Destroy(g, 2);

            gameObject.SetActive(false);
        }
    }
}
