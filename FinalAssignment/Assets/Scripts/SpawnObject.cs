using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject obj; // the game object spawned
    [SerializeField] private float incrementValue; // how much time before the object is spawned again
    [SerializeField] private float cooldownTimer; // how much time before the object is spawned again

    // Cooldown set to 0 at start
    private void Start()
    {
        cooldownTimer = 0;
    }

    // Spawns an object when the cooldown timer is at 0
    public void SpawnBullet()
    {
        if (cooldownTimer <= 0)
        {
            Instantiate(obj, transform.position, obj.transform.rotation);
            cooldownTimer += incrementValue;
        }
    }

    // Count down the cooldown timer
    public void CountTimer()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        else
            cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountTimer();
    }
}
