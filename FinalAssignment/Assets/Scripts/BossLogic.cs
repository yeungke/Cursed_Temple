using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private int bossHealth = 16;
    [SerializeField] private bool goLeft;

    // Define the boss movement boundaries when it spawns; begins by moving left
    void Start()
    {
        leftBoundary = 134f;
        rightBoundary = 155f;
        goLeft = true;
    }

    // When the boss collides with a player weapon, it takes damage and plays a damage sound
    public void OnTriggerEnter(Collider collider)
    {
        WeaponKnife knife = collider.gameObject.GetComponent<WeaponKnife>();
        WeaponAxe axe = collider.gameObject.GetComponent<WeaponAxe>();
        WeaponCross cross = collider.gameObject.GetComponent<WeaponCross>();

        if (knife != null || cross != null)
        { 
            bossHealth--;
            BossDamageSound();
        }

        if (axe != null)
        {
            bossHealth -= 3;
            BossDamageSound();
        }
    }

    // Damage sound is played only if the boss does not receive lethal damage
    private void BossDamageSound()
    {
        if (bossHealth > 0)
            FindObjectOfType<SoundManager>().Play("BossDamage");
    }

    // When the boss die, play a death sound, destroy the object, add to the score, spawn the treasure chest
    private void BossDestroyed()
    {
        if (bossHealth <= 0)
        {
            FindObjectOfType<SoundManager>().Play("BossDeath");
            Destroy(this.gameObject);
            GameManager.instance.score += 500;
            GameManager.instance.BossDies();
        }
    }

    // The boss moves along the z-axis until it hits the boundary, then moves the opposite direction
    private void BossMovement()
    {
        if (transform.position.z >= rightBoundary)
            goLeft = true;

        if (transform.position.z <= leftBoundary)
            goLeft = false;

        if (goLeft)
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        else if (!goLeft)
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        BossMovement();
        BossDestroyed();
    }
}
