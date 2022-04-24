using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDamage : MonoBehaviour
{
    [SerializeField] private float immuneValue = 0.75f;
    [SerializeField] private float immuneTimer;

    // Hero is not immune at spawn
    private void Start()
    {
        immuneTimer = 0;
    }

    // If the hero touches an enemy collider, call the TakeDamage() method
    public void OnTriggerEnter(Collider collider)
    {
        EnemyWalk enemy1 = collider.gameObject.GetComponent<EnemyWalk>();
        EnemyFly enemy2 = collider.gameObject.GetComponent<EnemyFly>();
        EnemyShoot enemy3 = collider.gameObject.GetComponent<EnemyShoot>();
        BossLogic boss = collider.gameObject.GetComponent<BossLogic>();

        TreasureChest treasure = collider.gameObject.GetComponent<TreasureChest>();

        // Hero only takes damage if not immune; taking damage adds time to the immunity timer
        if (immuneTimer <= 0)
        {
            if (enemy1 != null || enemy2 != null || enemy3 != null || boss != null)
            {
                TakeDamage();
                immuneTimer += immuneValue;
            }
        }

        // Touching the treasure ends the game, plays a victory jingle, and stops the boss theme
        if (treasure != null)
        {
            FindObjectOfType<SoundManager>().Play("Victory");
            GameManager.instance.GameWin();
            gameObject.SetActive(false);
            SoundManager.instance.Stop("BossTheme");
        }
    }

    // If the hero touches an enemy bullet, call the TakeDamage() method and destroy the bullet
    public void OnCollisionEnter(Collision collision)
    {
        EnemyBullet bullet = collision.gameObject.GetComponent<EnemyBullet>();

        // Hero only takes damage if not immune; taking damage adds time to the immunity timer
        if (immuneTimer <= 0)
        {
            if (bullet != null)
            {
                TakeDamage();
                immuneTimer += immuneValue;
                bullet.DestroyObject();
            }
        }
    }

    private void TakeDamage()
    {
        // Reduce the health value in GameManager by 1
        GameManager.instance.health--;

        // Only play the damage sound if the player did not die
        if (GameManager.instance.health > 0)
        {
            SoundManager.instance.Play("PlayerDamage");
        }

        // Check the GameManager to see if the hero's health is 0 or lower
        CheckHealth();
    }

    // When the health value in the GameManager hits 0, end the game
    private void CheckHealth()
    {
        // Death triggers a player death sound and deactivates the player object
        if (GameManager.instance.health <= 0)
        {
            FindObjectOfType<SoundManager>().Play("PlayerDeath");
            GameManager.instance.GameEnd();
            gameObject.SetActive(false);
        }
    }

    // Count down the immunity timer
    private void DamageImmuneTimer()
    {
        if (immuneTimer > 0)
            immuneTimer -= Time.deltaTime;
        else
            immuneTimer = 0;
    }    

    private void Update()
    {
        DamageImmuneTimer();
    }
}
