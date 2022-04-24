using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAxe : MonoBehaviour
{
    // Destroy the upgrade after the player collects it
    public void OnTriggerEnter(Collider trigger)
    {
        HeroLogic hero = trigger.gameObject.GetComponent<HeroLogic>();

        if (hero != null)
        {
            Destroy(gameObject);
        }
    }
}
