using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLogic : MonoBehaviour
{
    [SerializeField] private Vector2 axis; // used to retrieve the X-acis for keyboard or joystick
    [SerializeField] private float moveSpeed = 7.5f; // allows side-to-side movement, adjustable by developer
    [SerializeField] private Animator animator; // allows animations to play for corresponding inputs
    private Rigidbody rb; // used to retrieve the RigidBody

    /* The key code used for jump */
    [SerializeField] private KeyCode jumpKey = KeyCode.J;

    /* Variables used for jumping */
    [SerializeField] private Vector3 jump;
    [SerializeField] private float jumpForce = 35.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravityFloat = 3.25f;
    [SerializeField] private float lowJumpFloat = 2.5f;

    /* Variables used for climbing ladders */
    [SerializeField] private bool touchLadder;
    [SerializeField] private bool isClimbing;

    // Detects collision with walls
    [SerializeField] private bool wallToLeft;
    [SerializeField] private bool wallToRight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        touchLadder = false;
        isClimbing = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        FloorLogic floor = collision.gameObject.GetComponent<FloorLogic>();
        WallLogicLeft wallLeft = collision.gameObject.GetComponent<WallLogicLeft>();
        WallLogicRight wallRight = collision.gameObject.GetComponent<WallLogicRight>();

        // If the hero is touching the floor, they are grounded and not climbing a ladder
        if (floor != null)
        {
            isGrounded = true;
            isClimbing = false;
            animator.SetBool("ClimbBool", false);
        }

        // Detects player collision with walls
        if (wallLeft != null)
            wallToLeft = true;
        if (wallRight != null)
            wallToRight = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        WallLogicLeft wallLeft = collision.gameObject.GetComponent<WallLogicLeft>();
        WallLogicRight wallRight = collision.gameObject.GetComponent<WallLogicRight>();

        // Player cannot move left through a wall
        if (wallLeft != null)
            wallToLeft = false;

        // Player cannot move right through a wall
        if (wallRight != null)
            wallToRight = false;
    }

    public void OnTriggerEnter(Collider trigger)
    {
        LadderLogic ladder = trigger.gameObject.GetComponent<LadderLogic>();
        UpgradeKnife knifeBox = trigger.gameObject.GetComponent<UpgradeKnife>();
        UpgradeAxe axeBox = trigger.gameObject.GetComponent<UpgradeAxe>();
        UpgradeCross crossBox = trigger.gameObject.GetComponent<UpgradeCross>();

        // Detects collision between the hero and a ladder
        if (ladder != null)
            touchLadder = true;

        // Player picks up a new weapon, and plays the Upgrade sound
        if (knifeBox != null)
        {
            GameManager.instance.ActivateKnife();
            FindObjectOfType<SoundManager>().Play("Upgrade");
        }
        if (axeBox != null)
        {
            GameManager.instance.ActivateAxe();
            FindObjectOfType<SoundManager>().Play("Upgrade");
        }
        if (crossBox != null)
        {
            GameManager.instance.ActivateCross();
            FindObjectOfType<SoundManager>().Play("Upgrade");
        }
    }

    public void OnTriggerExit(Collider trigger)
    {
        LadderLogic ladder = trigger.gameObject.GetComponent<LadderLogic>();

        // Set touching and climbing to false when the hero is no longing touching a ladder
        if (ladder != null)
        {
            touchLadder = false;
            isClimbing = false;
            animator.SetBool("ClimbBool", false);
        }
    }

    private void HeroMovement()
    {
        axis.x = Input.GetAxis("Horizontal"); // the X-axis of the user input, between -1 and 1, 0 at neutral
        axis.y = Input.GetAxis("Vertical"); // the Y-axis of the user input, between -1 and 1, 0 at neutral
        Vector3 movementVector = new Vector3(0, 0, axis.x); // attach X-axis to movement vector

        if (animator != null)
        {
            animator.SetFloat("X-axis", axis.x);
            animator.SetFloat("Y-axis", axis.y);

            // Allow the player to run when not touching a wall
            if (!wallToLeft && !wallToRight || wallToLeft && axis.x > 0 || wallToRight && axis.x < 0)
            {
                // Translate the transform based on the speed and movement vector variable
                transform.Translate(movementVector * moveSpeed * Time.deltaTime, Space.World);
            }

            // X-axis input controls the running animation
            if (axis.x != 0)
                animator.SetBool("RunBool", true);
            else
                animator.SetBool("RunBool", false);

            // Determines grounded animations
            if (isGrounded == true)
                animator.SetBool("GroundBool", true);
            else
                animator.SetBool("GroundBool", false);

            // The hero turns to face the ladder while climbing
            if (isClimbing == true)
            {
                transform.forward = Vector3.left;
            }
            // Rotate the hero to face the direction it's moving toward on the X-axis
            else if (movementVector != Vector3.zero)
            {
                transform.forward = movementVector;
            }

            // Enable Hero to jump
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                isGrounded = false; // disables character from jumping while in the air
                rb.AddForce(jump * jumpForce, ForceMode.Impulse); // jump based on a vector and a force
                animator.SetTrigger("JumpTrigger");
            }

            // Enable Hero to climb up ladders, or climb up and down ladders if not touching the ground
            if (touchLadder && axis.y > 0 || touchLadder && !isGrounded)
            {
                transform.Translate(moveSpeed * axis.y * Time.deltaTime * transform.up, Space.World);
                isGrounded = false;
            }

            // Allows the player to jump onto the ladder and climb, so long as the Y-axis is not at 0
            if (touchLadder && axis.y != 0 && !isGrounded)
            {
                animator.SetBool("ClimbBool", true);
                isClimbing = true;
            }

            // If the player is not climbing a ladder, enable a vertical velocity for a less floaty jump
            if (!isClimbing)
            {
                // Force the player to fall faster when they reach the apex of their jump
                if (rb.velocity.y < 0)
                {
                    rb.velocity += (Physics.gravity.y * (gravityFloat - 1) * Time.deltaTime) * Vector3.up;
                }
                // Allow the player to do a short jump; the player will fall as soon as they let go of the Jump key
                else if (rb.velocity.y > 0 && !Input.GetKey(jumpKey))
                {
                    rb.velocity += (Physics.gravity.y * (lowJumpFloat - 1) * Time.deltaTime) * Vector3.up;
                }
            }
        }
    }

    // Code that allows players to climb a ladder
    private void LadderPhysics()
    {
        // Disable physics and stop all upward velocity when climbing a ladder
        if (isClimbing)
        {
            rb.useGravity = false;
            rb.velocity = 0 * Vector3.up;
            GameManager.instance.canAttack = false;
        }
        // Re-enable physics when the player is not climbing a ladder
        else
        {
            rb.useGravity = true;
            GameManager.instance.canAttack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LadderPhysics();
        HeroMovement();
    }
}
