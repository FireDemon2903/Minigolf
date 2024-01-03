using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float Speed = 8.0f;
    public Vector3 move;

    public bool freeCam = true;
    public Vector2 look;
    float yaw = 0.0f;
    float pitch = 0.0f;

    #region Inputs
    void OnMove(InputValue value)
    {
        move = value.Get<Vector3>();
    }

    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }
    #endregion Inputs

    private void Update()
    {
        var step = Speed * Time.deltaTime; // calculate distance to move
        Vector3 relativeMovement = transform.rotation * move;
        if (freeCam)
            transform.position += relativeMovement * step;

        if (Input.GetMouseButton(1))
        {
            float sensitivity = 0.1f; // Adjust this value to change the speed of rotation

            yaw += look.x * sensitivity;
            pitch -= look.y * sensitivity;

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        }
    }
}
