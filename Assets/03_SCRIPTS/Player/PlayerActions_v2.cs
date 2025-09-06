using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerActions_v2 : MonoBehaviour
{
    // 乁( ^（●●）^ )ㄏ Ralph is looking over this project 

    private PlayerControls playerControls;
    
    public Rigidbody rb;
    public GameObject cameraHolder;

    public float currentSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float sensitivity;
    public float maxForce;

    private Vector2 move;
    private Vector2 look;
    private float lookRotation;
    
    public bool viewingObject = false;
    public bool canMove = true;
    public bool canLookOnly = false;
    public Image Reticle;
    private Camera cam;

    public Sprite defaultCursor;
    public Sprite grabCursor;
    public Sprite doorCursor;
    public Sprite emptyCursor;

    //AUDIO//
    private SoundsManager soundsManager;
    public AudioSource player_steps_stone_source;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        soundsManager = SoundsManager.instance;

        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        GetComponent<MeshRenderer>().enabled = false; //disable capsule when playing
    }

    private void Update()
    {
        // Interaction Mechanic
        /*Ray ray = cam.ScreenPointToRay(playerControls.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
            //code here
            }
        }*/
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }
    
    private void Move()
    {
        if (!viewingObject && canMove)
        {
           // Set Player Speed
           if (playerControls.Player.Sprint.ReadValue<float>() == 1){
                // If Sprinting
                currentSpeed = sprintSpeed;
                cam.fieldOfView = 63f;
            } else if (playerControls.Player.Crouch.ReadValue<float>() == 1){
                // If Crouching
                transform.localScale = new Vector3(1,.35f,1);
                currentSpeed = walkSpeed / 2;
            } else {
                // Normal Movement
                transform.localScale = new Vector3(1,1,1);
                currentSpeed = walkSpeed;
                cam.fieldOfView = 60f;
            }

            //Find target velocity
            //Vector3 currentVelocity = rb.velocity;
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
            targetVelocity *= currentSpeed;

            //align direction
            targetVelocity = transform.TransformDirection(targetVelocity);

            //calculate forces
            Vector3 velocityChange = (targetVelocity - currentVelocity);
            //make sure we fall back to ground if we walk off a ledge
            velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

            //limit force
            Vector3.ClampMagnitude(velocityChange, maxForce);

            //add force to rigidbody
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
            
            //PLAY FOOTSTEPS SOUNDS
            if(rb.linearVelocity.magnitude > 0.5)
            {
                if (!player_steps_stone_source.isPlaying)
                {
                    player_steps_stone_source.Play();
                }
            }
            else
            {
                player_steps_stone_source.Stop();
            }
        }
    }
    
    private void Look()
    {
        if (!viewingObject && canMove || canLookOnly){

            if (Time.timeScale != 0)
            {
                //turn
                transform.Rotate(Vector3.up * look.x * sensitivity);

                //look
                lookRotation += (-look.y * sensitivity);
                lookRotation = Mathf.Clamp(lookRotation, -90, 90);
                cameraHolder.transform.eulerAngles = new Vector3(lookRotation, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
            }
        }
    }
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
