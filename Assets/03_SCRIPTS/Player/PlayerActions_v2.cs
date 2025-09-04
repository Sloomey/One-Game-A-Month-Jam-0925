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
        //Reticle.GetComponent<Image>().sprite = defaultCursor;
        //Reticle.color = new Color32(255,255,255,175);
        
        // Interaction Mechanic
        /*Ray ray = cam.ScreenPointToRay(playerControls.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3)){

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null){

                // Change Reticle color
                if (viewingObject){
                    Reticle.GetComponent<Image>().sprite = emptyCursor;
                    //Reticle.color = new Color32(255,255,255,175);
                } else {
                    Reticle.GetComponent<Image>().sprite = grabCursor;
                    //Reticle.GetComponent<Image>().sprite = grabCursor;
                    Reticle.color = new Color32(255,255,255,175);
                }

                // Hover door text
                Door _door = interactable as Door;
                if (_door != null){
                    if (gameManager.ItemHint2){
                        gameManager.ItemSellHint = false;
                        gameManager.ItemDropHint = false;
                    }
                    itemHint_Drop.SetActive(false);
                    itemHint_Sell.SetActive(false);
                    HoverDoor(_door);
                }
                
                // Attempting to Interact
                if (playerControls.Player.Interact.triggered && !viewingObject){
                    
                    Debug.Log("hit interactable item: " + interactable);
                    interactable.Interact();
                    
                    soundsManager.Play("invOpen");
                    player_steps_stone_source.Stop();

                    // Check if grabbing Key
                    if (interactable.gameObject.name == "Key"){
                        gameManager.ShowKeyUI();
                        gameManager.GetComponent<ItemManager>().pickUpItem(interactable.gameObject.name);
                        interactable.gameObject.SetActive(false);
                    }

                    // Check if the object we're interacting with is Item (vs door or etc)
                    Item _item = interactable as Item;
                    if (_item != null){
                        if (gameManager.ItemHint1) gameManager.ItemHint1 = false;
                        if (gameManager.ItemHint2){
                            gameManager.ItemSellHint = true;
                            gameManager.ItemDropHint = true;
                            //gameManager.ItemHint2 = false;
                        }
                        itemHint_Drop.SetActive(true);
                        itemHint_Sell.SetActive(true);

                        SetFocus(_item);
                    }

                    if (_door != null){
                        SelectDoor(_door);
                    }
                }
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
        /*else
        {
            // Player Controls when Viewing Object
            Item _item = currentFocus as Item;

            if (_item != null){
                // Place item back
                if (playerControls.Player.PlaceItem.ReadValue<float>() == 1){
                    viewingObject = false;
                    Item3DViewer viewer = GetComponent<Item3DViewer>();
                    viewer.StopDisplaying();
                    
                    // Return origonal object
                    //currentFocus.GetComponent<MeshRenderer>().enabled = true;
                    EnableMeshRecursive(currentFocus.transform);
                    currentFocus = null;

                    if (gameManager.ItemSellHint && gameManager.ItemDropHint){
                        gameManager.ItemSellHint = false;
                        gameManager.ItemDropHint = false;
                    }
                    
                    soundsManager.Play("invClose");
                }

                // Sell item
                if (playerControls.Player.SellItem.ReadValue<float>() == 1){
                    
                    // Increase money
                    soundsManager.Play("sell");
                    
                    gameManager.money += _item.sellPrice;
                    gameManager.UpdateMoneyUI();
                    gameManager.GetComponent<ItemManager>().pickUpItem(_item.gameObject.name);
                    Debug.Log(_item + " + " + _item.gameObject + " + " + _item.gameObject.name);

                    Debug.Log("Selling item for: " + _item.sellPrice);
                    Debug.Log("new total money should be: " + gameManager.money);
                    
                    viewingObject = false;
                    Item3DViewer viewer = GetComponent<Item3DViewer>();
                    viewer.StopDisplaying();

                    // Destroy origonal object (dont actually destroy, just set to inactive)
                    // - we need to keep track of these for itemmanager
                    if (gameManager.ItemSellHint && gameManager.ItemDropHint){
                        gameManager.ItemSellHint = false;
                        gameManager.ItemDropHint = false;
                    }

                    // Destroy(currentFocus.gameObject);
                    currentFocus.gameObject.SetActive(false);
                    currentFocus = null;
                }
            }*/

            // Player Controls when Viewing Door UI
            /*Door _door = currentFocus as Door;
            if (_door != null){
                
                // CHECK IF WE HAVE ENOUGH MONEY, ELSE THROW UI ERROR
                
                // Enter Door Scene
                if (playerControls.Player.EnterDoor.ReadValue<float>() == 1){
                    viewingObject = false;
                    currentFocus = null;

                    _door.LoadContainerScene();
                }

                // Exit Door Focus
                if (playerControls.Player.ExitDoor.ReadValue<float>() == 1){
                    viewingObject = false;
                    currentFocus = null;
                    
                    // Turn off UI
                }
            }
        }*/
    }

    /*
    private void EnableMeshRecursive(Transform parent)
    {
        MeshRenderer meshRenderer = parent.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            EnableMeshRecursive(parent.GetChild(i));
        }
    }*/
    
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
    
    /*
    private void SetFocus(Interactable newFocus)
    {
        viewingObject = true;
        currentFocus = newFocus;
        Item3DViewer viewer = GetComponent<Item3DViewer>();
        viewer.Display(newFocus as Item);

        Item _item = newFocus as Item;
        DoorUICanvas.enabled = true;
        DoorUICanvas.GetComponent<DoorUIViewer>().updateNameText(_item.name);
        DoorUICanvas.GetComponent<DoorUIViewer>().updateDescText(_item.description);
        DoorUICanvas.GetComponent<DoorUIViewer>().updatePriceText("Value: $" + _item.sellPrice.ToString());
    }
    

    private void DropFocus(Interactable newFocus){
        currentFocus = null;
    }

    private void HoverDoor(Door newDoor)
    {
        Reticle.GetComponent<Image>().sprite = doorCursor;
        Reticle.color = new Color32(255,255,255,215);
        DoorUICanvas.enabled = true;
        DoorUICanvas.GetComponent<DoorUIViewer>().updateNameText(newDoor.containerName);
        DoorUICanvas.GetComponent<DoorUIViewer>().updateDescText(newDoor.containerDescription);

        // Check if we've opened door before, and if not show price
        if (gameManager.openedDoors.Contains(newDoor.containerName)){
            DoorUICanvas.GetComponent<DoorUIViewer>().updatePriceText("OWNED");
            newDoor.containerPrice = 0;
            newDoor.containerDescription = "Status: Unlocked";
        } else {
            DoorUICanvas.GetComponent<DoorUIViewer>().updatePriceText("Bid Required: $" + newDoor.containerPrice.ToString());
        }

        // Make special case for container 8 that shows Locked instead of price based
        if (newDoor.containerName == "Container 08"){
            DoorUICanvas.GetComponent<DoorUIViewer>().updatePriceText("LOCKED");
        }
        
        if (newDoor.isExit){
            DoorUICanvas.GetComponent<DoorUIViewer>().updatePriceText("");
        }
    }

    private void SelectDoor(Door newDoor)
    {
        // Make special case for container 8 - need key insetad of money
        if (newDoor.containerName == "Container 08" && gameManager.haskey){
            gameManager.HideKeyUI();
            newDoor.LoadContainerScene();
        }
        
        // Enter Door
        if (gameManager.money < newDoor.containerPrice){
            // Throw some kind of error or something
            Debug.Log("insufficient funds");
        } else {
            
            gameManager.money -= newDoor.containerPrice;
            gameManager.UpdateMoneyUI();
            newDoor.hasOpened = true;
            if (!gameManager.openedDoors.Contains(newDoor.containerName) && !newDoor.isExit){
                gameManager.addDoorToOpenList(newDoor.containerName);
            } 
            newDoor.LoadContainerScene();
        }
    }
    */
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}