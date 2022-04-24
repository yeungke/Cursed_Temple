using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 3.5f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float topBoundary;
    [SerializeField] private float botBoundary;
    [SerializeField] private bool flyUp;
    [SerializeField] private int enemyHealth = 1;

    // Define the enemy movement boundaries when it spawns; begins by moving left
    private void Start()
    {
        topBoundary = transform.position.y + 2;
        botBoundary = transform.position.y - 2;
        flyUp = true;
    }

    // When the enemy collides with a player weapon, it takes damage
    public void OnTriggerEnter(Collider collider)
    {
        WeaponKnife knife = collider.gameObject.GetComponent<WeaponKnife>();
        WeaponAxe axe = collider.gameObject.GetComponent<WeaponAxe>();
        WeaponCross cross = collider.gameObject.GetComponent<WeaponCross>();

        if (knife != null || cross != null)
            enemyHealth--;

        if (axe != null)
            enemyHealth -= 3;
    }

    // When the enemy health hits 0, play a death sound, destroy the object, and add to the score
    private void EnemyDestroyed()
    {
        if (enemyHealth <= 0)
        {
            FindObjectOfType<SoundManager>().Play("EnemySkull");
            DestroyObject();
            GameManager.instance.score += 20;
        }
    }

    // Method to destroy the enemy object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // The enemy moves along the y-axis until it hits the boundary, then moves the opposite direction
    // The enemy moves consistently along the z-axis
    private void ZigZagMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

        if (transform.position.y > topBoundary)
            flyUp = false;

        if (transform.position.y < botBoundary)
            flyUp = true;

        if (flyUp)
            transform.Translate(Vector3.up * Time.deltaTime * verticalSpeed);
        else if (!flyUp)
            transform.Translate(Vector3.down * Time.deltaTime * verticalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ZigZagMovement();
        EnemyDestroyed();
    }
}
