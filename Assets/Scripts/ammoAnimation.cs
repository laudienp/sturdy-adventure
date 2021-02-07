using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoAnimation : MonoBehaviour
{
    public float speed = 20f;
    public float amplitude = 0.25f;

    private float ypos;

    // Start is called before the first frame update
    void Start()
    {
        ypos = transform.position.y;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * amplitude + ypos, transform.position.z);
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
