using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool follow;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       
    }

    // Update is called once per frame
    void Update()
    {
        if(follow)
        {
            transform.position = new Vector3(player.position.x, 0, player.position.z);
        }
    }
}
