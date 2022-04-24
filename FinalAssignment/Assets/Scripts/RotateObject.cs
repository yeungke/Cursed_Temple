using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3;
    [SerializeField] private Vector3 vector;

    // Rotates an object based on a vector and speed
    private void RotateLogic()
    {
        transform.Rotate(vector * Time.deltaTime, rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        RotateLogic();
    }
}
