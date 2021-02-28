using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float firerate = 0.5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public GameObject shotSound;
    public GameObject emptyShotSound;

    public GameObject hitWallEffect;

    public Animator anim;

    float lastShot;

    public float ammo;

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

            if(ammo > 0)
            {
                Ray center = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(center, out RaycastHit hit, 1000))
                {
                    if (hit.collider.tag.Equals("Monster"))
                        hit.collider.GetComponent<Damagable>().TakeDamage(20);
                    else //hit a wall
                    {
                        Instantiate(hitWallEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    }

                    anim.SetTrigger("Shoot");
                }

                //sound
                GameObject g = Instantiate(shotSound, transform.position, Quaternion.identity);
                Destroy(g, 5);

                ammo--;
            }
            else
            {
                GameObject g = Instantiate(emptyShotSound, transform.position, Quaternion.identity);
                Destroy(g, 5);
            }

            

            lastShot = Time.time + firerate;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            reloading = true;
            
        }
    }

    public void Ammo(int ammo)
    {
        this.ammo += ammo;
    }

    public void OnEndReload()
    {
        reloading = false;
        ammo = 10;
    }
}
