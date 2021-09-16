using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float actualSpeed;
    public float gravity = -9.81f;
    public float gravityMultiplier = 2.5f;
    public float jumpHeight = 2;
    public bool grounded;

    public float mouseSpeed;

    public Transform head;

    private CharacterController controller;

    private GameCheckpoint lastCheckpoint;

    private PlayerHUD hud;

    private float xview;
    private float yview;

    private float yvel = 0;

    Health health;

    public bool freezeControl;
    private bool dead;

    private InputMaster input;

    private void Awake()
    {
        input = new InputMaster();
        input.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        hud = GetComponent<PlayerHUD>();
        health.onDie += Die;

        xview = transform.rotation.eulerAngles.y; //take the current object rotation

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeControl)
            return;

        float x = input.Player.Movement.ReadValue<Vector2>().x;
        float y = input.Player.Movement.ReadValue<Vector2>().y;

        float mx = input.Player.Camera.ReadValue<Vector2>().x;
        float my = input.Player.Camera.ReadValue<Vector2>().y;

        //cam
        xview += mx * mouseSpeed;
        yview -= my * mouseSpeed;
        yview = Mathf.Clamp(yview, -90, 90);
        transform.eulerAngles = new Vector3(0, xview, 0);
        head.localEulerAngles = new Vector3(yview, 0, 0);

        //dep
        yvel += Time.deltaTime * gravity * gravityMultiplier;

        if (input.Player.Run.ReadValue<float>()>0) actualSpeed = runSpeed;
        else actualSpeed = walkSpeed;

        Vector3 movement = transform.right * x + transform.forward * y;
        Vector3 velocity = movement * actualSpeed + Vector3.up * yvel;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            yvel = 0;
            grounded = true;
        }
    }

    private void Jump()
    {
        if (!grounded)
            return;

        yvel = Mathf.Sqrt(-2 * gravity * jumpHeight * gravityMultiplier);
        grounded = false;
    }

    void Die()
    {
        dead = true;
        freezeControl = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        dead = false;
        controller.enabled = false;

        if (lastCheckpoint)
            transform.position = lastCheckpoint.transform.position;
        else
            transform.position = Vector3.zero;

        controller.enabled = true;
        health.Full();
        freezeControl = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        RenderSettings.fogDensity = 0.005f; //to avoid bug
    }

    public void SetRespawn(GameCheckpoint cp) // faire une notif sur l'UI
    {
        if (lastCheckpoint != cp)
        {
            lastCheckpoint = cp;
            hud.CheckpointPassed();
        }
    }


    public void FadeTeleport(Transform position)
    {
        DOTween.Sequence()
            .Append(hud.loadingFade.DOFade(1, 0.5f).OnComplete(() =>TeleportTo(position)))
            .Append(hud.loadingFade.DOFade(1, 1f))
            .Append(hud.loadingFade.DOFade(0, 0.5f));
    }

    public void TeleportTo(Transform position)
    {
        controller.enabled = false;
        transform.position = position.position;
        xview = position.rotation.eulerAngles.y; // rotate player to spawn forward
        controller.enabled = true;
    }

    public bool IsDead()
    {
        return dead;
    }
}
