using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Speed of the bullet; default at 12
    [SerializeField] private float speed = 12f;

    // Method called to destroy the object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Sends the projectile forward
    private void MoveForward()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }
}
