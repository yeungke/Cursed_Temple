using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RayCastLogic : MonoBehaviour
{
    [SerializeField] private bool debug;
    [SerializeField] private Color col;
    [SerializeField] private Color openCol;
    [SerializeField] private Color closedCol;

    // Raycast State
    [SerializeField] private Vector3 vDir; // direction to check
    [SerializeField] private float distance; // how long the raycast is
    public bool hit; // changes when the raycast hits the player
    public LayerMask layer;

    public UnityEvent OnRaycastHit; // event to fire when raycast hits player

    public void RayCastUpdate()
    {
        RaycastHit hitInfo;
        Vector3 vStart = transform.position;
        Vector3 vEnd = vStart + distance * vDir;

        col = openCol;
        hit = Physics.Raycast(vStart, vDir, out hitInfo, distance, layer);

        // If the raycast hits an object (the player in this case), invoke a method
        if (hit == true)
        {
            HeroLogic hero = hitInfo.collider.GetComponent<HeroLogic>();
            col = closedCol;
            if (OnRaycastHit != null && hero != null)
            {
                OnRaycastHit.Invoke();
            }
        }

        Debug.DrawLine(vStart, vEnd, col);
    }

    // Update is called once per frame
    void Update()
    {
        RayCastUpdate();
    }
}
