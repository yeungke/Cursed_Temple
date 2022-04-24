using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 4;

    // When the enemy collides with a player weapon, it takes damage
    public void OnTriggerEnter(Collider collider)
    {
        WeaponKnife knife = collider.gameObject.GetComponent<WeaponKnife>();
        WeaponAxe axe = collider.gameObject.GetComponent<WeaponAxe>();
        WeaponCross cross = collider.gameObject.GetComponent<WeaponCross>();

        if (knife != null || cross != null)
        {
            enemyHealth--;
            if (enemyHealth > 0)
                SoundManager.instance.Play("EnemyTurretDamaged");
        }

        if (axe != null)
        {
            enemyHealth -= 3;
            if (enemyHealth > 0)
                SoundManager.instance.Play("EnemyTurretDamaged");
        }
    }

    // When the enemy health hits 0, play a death sound, destroy the object, and add to the score
    private void EnemyDestroyed()
    {
        if (enemyHealth <= 0)
        {
            DestroyObject();
            GameManager.instance.score += 40;
            FindObjectOfType<SoundManager>().Play("EnemyTurret");
        }
    }

    // Destroys this game object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDestroyed();
    }
}
