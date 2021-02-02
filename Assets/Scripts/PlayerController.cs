using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    public float gravity = -9.81f;
    public float gravityMultiplier = 2.5f;
    public float jumpHeight = 2;
    public bool grounded;

    public float mouseSpeed;

    public Transform head;

    private CharacterController controller;

    private float xview;
    private float yview;

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

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");


        //cam
        xview += mx * mouseSpeed;
        yview -= my * mouseSpeed;
        yview = Mathf.Clamp(yview, -90, 90);
        transform.eulerAngles = new Vector3(0, xview, 0);
        head.localEulerAngles = new Vector3(yview, 0, 0);


        //dep
        yvel += Time.deltaTime * gravity * gravityMultiplier;

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            yvel = Mathf.Sqrt(-2 * gravity * jumpHeight * gravityMultiplier);
            grounded = false;
        }
            

        Vector3 movement = transform.right * x + transform.forward * y;
        Vector3 velocity = movement * speed + Vector3.up * yvel;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            yvel = 0;
            grounded = true;
        }
            
    }
}
