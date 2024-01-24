using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    // :)

    // Multiplayer
    int targetIndex = 0;
    public List<Transform> targets = new();
    Transform targetObject;

    // Freecam
    bool freeCam = false;
    float Speed = 16.0f;
    Vector3 move;

    // Mouse movement in 2D relative to screen, not world
    Vector2 DeltaLook;

    // cam rotation
    float yaw;
    float pitch;

    // Distance to taget in not freecam mode
    float camDist = 15f;

    // For input system
    #region Inputs
    void OnMove(InputValue value) { move = value.Get<Vector3>(); }
    void OnLook(InputValue value) { DeltaLook = value.Get<Vector2>(); }
    void OnToggleCam()
    {
        // If the player was previously in freecam mode, and changed it
        if (freeCam)
        {
            // Calculate the direction vector from the camera to the target object
            Vector3 directionToTarget = targetObject.position - transform.position;

            // Normalize the direction vector and multiply by the desired distance to get the offset
            Vector3 offset = directionToTarget.normalized * camDist;

            // Calculate the desired position of the camera
            Vector3 desiredPosition = targetObject.position + offset;

            // Set position and rotation
            transform.SetPositionAndRotation(desiredPosition, Quaternion.LookRotation(directionToTarget));
        }

        // Toggle freecam
        freeCam = !freeCam;
    }
    void OnScroll(InputValue value)
    {
        float delta = value.Get<Vector2>().y * .02f;
        camDist -= camDist - delta < 4 ? 0 : delta;
    }
    #endregion Inputs

    // Called through "SendMessage" in other scripts
    void NextBall()
    {
        // Wrap
        targetIndex = targetIndex++ >= targets.Count - 1 ? 0 : targetIndex++;

        // Deactivate current active player
        targetObject.GetComponent<PlayerInput>().enabled = false;

        // Set
        targetObject = targets[targetIndex];

        // Enable controls
        targetObject.GetComponent<PlayerInput>().enabled = true;

        print("Changed player");
    }

    // Called in gamemanager
    void Begin()
    {
        // Set first target
        targetObject = targets[targetIndex];
        targetObject.GetComponent<PlayerInput>().enabled = true;

        // set pich and yaw
        yaw = Camera.main.transform.rotation.x;
        pitch = Camera.main.transform.rotation.y;
    }

    private void Update()
    {
        var step = Speed * Time.deltaTime; // calculate distance to move

        Vector3 relativeMovement = transform.rotation * move;
        
        if (freeCam)
        {
            // Movement
            transform.position += relativeMovement * step;
        }
        else 
        {
            // y-axis input
            transform.RotateAround(targetObject.transform.position, transform.right, 45 * move.z * Time.deltaTime);

            // x-axis input
            transform.RotateAround(targetObject.transform.position, Vector3.up, 45 * move.x * Time.deltaTime);

            // Calculate the direction vector from the target to the camera
            Vector3 directionToCamera = transform.position - targetObject.position;

            // Normalize the direction vector and multiply by the desired distance to get the offset
            Vector3 offset = directionToCamera.normalized * camDist;

            // Calculate the desired position of the camera
            Vector3 desiredPosition = targetObject.position + offset;

            // Set position and rotation
            transform.SetPositionAndRotation(desiredPosition, Quaternion.LookRotation(-directionToCamera));
        }

        if (Input.GetMouseButton(1))
        {
            float sensitivity = 0.1f; // Adjust this value to change the speed of rotation

            yaw += DeltaLook.x * sensitivity;
            pitch -= DeltaLook.y * sensitivity;

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
