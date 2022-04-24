using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimed : MonoBehaviour
{
    [SerializeField] private GameObject obj; // the game object spawned
    [SerializeField] private float cooldown = 0; // cooldown timer used to spawn an object
    [SerializeField] private float incrementValue = 3; // how much time before the object is spawned again

    private void SpawnTimedObject()
    {
        // Compares the firstSpawn to the game timer; only spawns if the game is not over
        if (cooldown <= 0 && GameManager.instance.isGameOver == false)
        {
            // adds a number to the cooldown timer (3 by default)
            cooldown += incrementValue;
            // instantiate the game object, using the prefab rotation
            Instantiate(obj, transform.position, obj.transform.rotation);
        }
    }

    // Count down the cooldown timer
    private void Countdown()
    {
        cooldown -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimedObject();
        Countdown();
    }
}
