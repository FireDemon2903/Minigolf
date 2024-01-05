using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public bool freeCam = false;

    public Transform targetObject;

    // Freecam
    public float Speed = 8.0f;
    public Vector3 move;


    public Vector2 look;
    float yaw;
    float pitch;

    // Distance to taget in not freecam mode
    float camDist = 15f;

    #region Inputs
    void OnMove(InputValue value) { move = value.Get<Vector3>(); }
    void OnLook(InputValue value) { look = value.Get<Vector2>(); }
    void OnToggleCam(InputValue value)
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
            transform.SetPositionAndRotation(-desiredPosition, Quaternion.LookRotation(directionToTarget));
        }

        // Toggle freecam
        freeCam = !freeCam;
    }
    #endregion Inputs

    private void Start()
    {
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
            transform.position += relativeMovement * step;
        }
        else 
        {
            // y-axis: TODO
            //transform.RotateAround(targetObject.transform.position, );

            // x-axis
            transform.RotateAround(targetObject.transform.position, Vector3.up, 45 * move.x * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            float sensitivity = 0.1f; // Adjust this value to change the speed of rotation

            yaw += look.x * sensitivity;
            pitch -= look.y * sensitivity;

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
