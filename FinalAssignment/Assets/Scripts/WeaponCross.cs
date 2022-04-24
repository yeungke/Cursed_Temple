using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCross : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float distance = 11f;
    [SerializeField] private float obj_distance = 0;
    [SerializeField] private bool returned;

    // Spawns with a distance of 0; has not returned to the player yet
    void Start()
    {
        obj_distance = 0;
        returned = false;
    }

    // Upon returning, the cross destroys itself when it collides with the hero
    public void OnTriggerEnter(Collider collider)
    {
        HeroLogic hero = collider.gameObject.GetComponent<HeroLogic>();

        if (hero != null && returned)
            DestroyObject();
    }

    // Destroys this game object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // Moves the cross in one direction, then moves back after it travelled a certain distance
    private void MoveCross()
    {
        float travel = Time.deltaTime * speed;

        if (!returned)
        {
            transform.Translate(Vector3.forward * travel); // moves object
            obj_distance += travel; // update object's distance travelled
            returned = obj_distance >= distance; // reverse the object's direction once it meets the distance
        }
        else
        {
            transform.Translate(Vector3.forward * -travel); // moves object backwards
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCross();
    }
}
