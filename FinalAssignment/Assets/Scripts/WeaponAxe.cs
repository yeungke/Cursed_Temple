using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAxe : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 6f;
    [SerializeField] private float upwardSpeed = 8f;
    [SerializeField] private float boundary;
    [SerializeField] private bool axeDrop;

    // Spawns with a boundary; the axe has not fallen yet
    private void Start()
    {
        boundary = transform.position.y + 5f;
        axeDrop = false;
    }

    // Destroys this game object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // The axe moves up until it hits a boundary, then drops back down
    private void MoveAxe()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        if (transform.position.y > boundary)
            axeDrop = true;

        if (!axeDrop)
            transform.Translate(Vector3.up * Time.deltaTime * upwardSpeed);
        if (axeDrop)
            transform.Translate(Vector3.down * Time.deltaTime * upwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAxe();
    }
}
