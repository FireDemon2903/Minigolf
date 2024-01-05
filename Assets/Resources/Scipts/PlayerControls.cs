using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float forceToAdd = 1f;

    Rigidbody targetRB;
    Vector3 TargetVelocity => targetRB.velocity;

    // Find stuff
    private void Start()
    {
        targetRB = GameObject.Find("Ball").GetComponent<Rigidbody>();
    }

    // mostly debugging
    private void Update()
    {
        // Find direction
        Vector3 direction = targetRB.transform.position - Camera.main.transform.position;

        // Find horizontal direction and normalize
        Vector2 horizontalDirection = new Vector2(direction.x, direction.z).normalized;

        // Draw line
        Debug.DrawLine(targetRB.transform.position, targetRB.transform.position + new Vector3(horizontalDirection.x, 0, horizontalDirection.y) * 5, Color.red);
    }

    void OnFire(InputValue Value)
    {
        if (TargetVelocity != Vector3.zero)
        {
            print("ball is moving, cannot fire");
        }
        else
        {
            // Find direction
            Vector3 direction = targetRB.transform.position - Camera.main.transform.position;

            // Find horizontal direction and normalize
            Vector2 horizontalDirection = new Vector2(direction.x, direction.z).normalized;

            // Add the force
            targetRB.AddForce(new Vector3(horizontalDirection.x, 0, horizontalDirection.y) * forceToAdd, ForceMode.Impulse);
        }
    }

    void OnFire2(InputValue Value)
    {
        targetRB.velocity = Vector3.zero;
        targetRB.angularDrag = 0f;
        targetRB.angularVelocity = Vector3.zero;
    }


}