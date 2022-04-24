using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject cross;
    [SerializeField] private KeyCode key = KeyCode.K;
    [SerializeField] private Animator animator;

    // Times the weapon with the throw animation, set a weapon cooldown (to prevent spamming)
    [SerializeField] private float attackBuffer = 0.5f;
    [SerializeField] private float cooldownTimer = 0;
    [SerializeField] private float setTimer = 1.5f;

    // Allows the player to attack; queues the animation and adds to the cooldown
    private void ThrowWeapon()
    {
        if (Input.GetKeyDown(key) == true && cooldownTimer == 0 && GameManager.instance.canAttack == true)
        {
            animator.SetTrigger("AttackTrigger");
            Invoke("LaunchProjectile", attackBuffer);
            cooldownTimer += setTimer;
        }
    }

    // Throws a projectile according to the current weapon
    private void LaunchProjectile()
    {
        if (GameManager.instance.isKnifeActive)
        {
            Instantiate(knife, transform.position, transform.rotation);
            FindObjectOfType<SoundManager>().Play("Knife");
        }

        if (GameManager.instance.isAxeActive)
        {
            Instantiate(axe, transform.position, transform.rotation);
            FindObjectOfType<SoundManager>().Play("Axe");
        }

        if (GameManager.instance.isCrossActive)
        {
            Instantiate(cross, transform.position, transform.rotation);
            FindObjectOfType<SoundManager>().Play("Cross");
        }
    }

    // Count down the cooldown timer
    private void CountCooldown()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        else
            cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowWeapon();
        CountCooldown();
    }
}
