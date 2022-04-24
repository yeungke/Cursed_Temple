using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKnife : MonoBehaviour
{
    [SerializeField] private float speed = 15f; // speed of the knife projectile

    //Colliding with an enemy or boss destroys the knife; it only hits one enemy at a time
    public void OnTriggerEnter(Collider collider)
    {
        EnemyWalk enemy1 = collider.gameObject.GetComponent<EnemyWalk>();
        EnemyFly enemy2 = collider.gameObject.GetComponent<EnemyFly>();
        EnemyShoot enemy3 = collider.gameObject.GetComponent<EnemyShoot>();
        BossLogic boss = collider.gameObject.GetComponent<BossLogic>();

        if (enemy1 != null || enemy2 != null || enemy3 != null || boss != null)
            DestroyObject();
    }

    // Method called to destroy the object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Sends the projectile forward
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }
}
