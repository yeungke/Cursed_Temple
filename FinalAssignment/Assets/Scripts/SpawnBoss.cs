using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;

    // Spawns the boss only once; plays the boss theme
    public void SpawnTheBoss()
    {
        if (!GameManager.instance.bossFightStarted)
        {
            Instantiate(bossObject, transform.position, bossObject.transform.rotation);
            SoundManager.instance.Stop("LevelTheme");
            SoundManager.instance.Play("BossTheme");
            GameManager.instance.bossFightStarted = true;
        }
    }
}
