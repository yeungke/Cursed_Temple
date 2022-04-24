using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasSoundPlayed = false;

    // retrieves the RigidBody of the treasure chest on Start
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        FloorLogic floor = collider.gameObject.GetComponent<FloorLogic>();

        // The chest falls from the sky until it touches the floor or goes below 2.5 on the global y-axis
        if (floor != null || transform.position.y <= 2.5)
        {
            // Stop the treasure chest from falling
            rb.useGravity = false;
            rb.isKinematic = true;

            // Play a thudding sound effect once
            if (!hasSoundPlayed)
            {
                FindObjectOfType<SoundManager>().Play("Slap");
                hasSoundPlayed = true;
            }

            // Set the transform of the treasure chest to 2.5 (just to make it look nicer)
            Vector3 reposition = transform.position;
            reposition.y = 2.5f;
            transform.position = reposition;
        }
    }

    // Activates the treasure; treasure object is set to inactive at the start
    public void ActivateTreasure()
    {
        gameObject.SetActive(true);
    }
}
