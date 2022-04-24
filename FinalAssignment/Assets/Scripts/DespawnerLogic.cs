using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerLogic : MonoBehaviour
{
    // If an enemy or a player weapon collides with the despawner wall, delete the object
    public void OnTriggerEnter(Collider collider)
    {
        EnemyWalk enemy1 = collider.gameObject.GetComponent<EnemyWalk>();
        EnemyFly enemy2 = collider.gameObject.GetComponent<EnemyFly>();
        EnemyBullet enemyBullet = collider.gameObject.GetComponent<EnemyBullet>();
        
        WeaponKnife knife = collider.gameObject.GetComponent<WeaponKnife>();
        WeaponAxe axe = collider.gameObject.GetComponent<WeaponAxe>();
        WeaponCross cross = collider.gameObject.GetComponent<WeaponCross>();

        if (enemy1 != null)
            enemy1.DestroyObject();

        if (enemy2 != null)
            enemy2.DestroyObject();

        if (enemyBullet != null)
            enemyBullet.DestroyObject();

        if (knife != null)
            knife.DestroyObject();

        if (axe != null)
            axe.DestroyObject();

        if (cross != null)
            cross.DestroyObject();
    }
}
