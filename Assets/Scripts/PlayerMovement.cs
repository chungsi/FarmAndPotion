using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody rb;

    private Vector3 movement;

    public Animator animator;
    bool movingLeft, movingRight;

    //for interact UI icon
    public GameObject interactUI;
    private Vector3 hitSize = new Vector3(1f, 1f, 1f);


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        movingLeft = false;
        movingRight = false;

        interactUI.SetActive(false);
    }

    void Update()
    {
        // Process input.
        movement.x = Input.GetAxisRaw("Horizontal"); // gives -1 to 1
        movement.z = Input.GetAxisRaw("Vertical");
        // movement.y = 0; // always zero??? maybe not if there are slopes are such
        movement.Normalize();

        //update character animation when moving
        if (movement.x == -1 || movement.z != 0)
        {
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }

        if (movement.x == 1 || movement.z != 0)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        animator.SetBool("MoveLeft", movingLeft);
        animator.SetBool("MoveRight", movingRight);

        //let player trigger interaction when pressing key
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
            Debug.Log("you pressed E");
        }
    }

    void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //check if character will trigger interactions
    public void OpenInteractableIcon()
    {
        interactUI.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactUI.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, hitSize, 0F);
        if(hits.Length >0)
        {
          foreach(RaycastHit rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                }
            }
        }

    }
}
      

