using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public float firerate = 0.5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && lastShot < Time.time)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(projectileSpawnPoint.forward));
            //faire un raycast
            lastShot = Time.time + firerate;
        }
    }
}
