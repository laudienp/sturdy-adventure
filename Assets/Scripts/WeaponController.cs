using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float bulletDamage = 20f;
    public float firerate = 0.5f;
    public int ammoPerCharger = 10;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public GameObject shotSound;
    public GameObject emptyShotSound;

    public GameObject hitWallEffect;

    public Animator anim;
    public PlayerHUD hud;

    float lastShot;

    public int currentCharger;
    public int ammo;

    public bool reloading;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && lastShot < Time.time && !reloading)
        {
            //Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(projectileSpawnPoint.forward));
            //faire un raycast

            if(currentCharger > 0)
            {
                Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(center, out RaycastHit hit, 1000))
                {
                    if (hit.collider.tag.Equals("Monster"))
                        hit.collider.GetComponent<Damagable>().TakeDamage(bulletDamage);
                    else //hit a wall
                    {
                        Instantiate(hitWallEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                }

                anim.SetTrigger("Shoot");

                //sound
                GameObject g = Instantiate(shotSound, transform.position, Quaternion.identity);
                Destroy(g, 5);

                currentCharger--;

                UpdateHUD();
            }
            else
            {
                GameObject g = Instantiate(emptyShotSound, transform.position, Quaternion.identity);
                Destroy(g, 5);
            }

            

            lastShot = Time.time + firerate;
        }

        if(Input.GetKeyDown(KeyCode.R) && ammo > 0 && currentCharger != ammoPerCharger)
        {
            anim.SetTrigger("Reload");
            reloading = true;
            
        }
    }

    public void Ammo(int ammo)
    {
        this.ammo += ammo;

        UpdateHUD();
    }

    public void OnEndReload()
    {
        reloading = false;
        int ammoReloaded;

        if (ammo >= ammoPerCharger)
        {
            ammoReloaded = ammoPerCharger;
            ammo -= ammoPerCharger;
        }
        else
        {
            ammoReloaded = ammo;
            ammo = 0;
        }

        currentCharger = ammoReloaded;

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (hud != null)
            hud.UpdateAmmo(currentCharger, ammo);
    }
}
