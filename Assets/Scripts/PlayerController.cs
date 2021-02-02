using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2;
    public bool grounded;
    private CharacterController controller;

    private float yvel = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        yvel += Time.deltaTime * gravity;

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            yvel = Mathf.Sqrt(-2 * gravity * jumpHeight);
            grounded = false;
        }
            

        Vector3 movement = new Vector3(x, 0, y);
        Vector3 velocity = movement * speed + Vector3.up * yvel;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            yvel = 0;
            grounded = true;
        }
            
    }
}
