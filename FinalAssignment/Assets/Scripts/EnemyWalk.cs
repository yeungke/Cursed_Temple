using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    [SerializeField] private Animator animator; // used to retrieve the zombie's animator controller
    [SerializeField] private float speed = 2f; // speed of the enemy's movement
    [SerializeField] private int enemyHealth = 2;
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool pointsCounted = false;
    [SerializeField] private bool soundPlayed = false;
    [SerializeField] private Collider zombieCollider;

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

    // When the enemy health hits 0, change a boolean value
    private void EnemyDestroyed()
    {
        if (enemyHealth <= 0)
        {
            isDead = true;

            // Add to the score once only
            if (!pointsCounted)
            {
                GameManager.instance.score += 10;
                pointsCounted = true;
            }

            // Play the death sound once only
            if (!soundPlayed)
            {
                FindObjectOfType<SoundManager>().Play("EnemyZombie");
                soundPlayed = true;
            }
        }
    }

    // Destroys this game object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Propels the obstacle without constant force
    private void MoveForward()
    {
        // If the enemy is not dead, move along the z-axis
        if (!isDead)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        // If the enemy is dead, call the death animation and turn off the collider
        // Destroy the object after the animation ends
        else
        {
            animator.SetTrigger("DeathTrigger");
            zombieCollider.enabled = false;
            Destroy(gameObject, 3.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        EnemyDestroyed();
    }
}
