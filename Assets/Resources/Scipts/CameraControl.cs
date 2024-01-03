using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float Speed = 8.0f;

    public Vector3 inp;

    void OnMove(InputValue value)
    {
        inp = value.Get<Vector3>();
    }

    private void Update()
    {
        var step = Speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.position+inp, step);
    }


}
