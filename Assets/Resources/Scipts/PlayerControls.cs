using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float forceToAdd = 1f;

    Rigidbody targetRB;
    Vector3 TargetVelocity => targetRB.velocity;
    bool isMoving => TargetVelocity != Vector3.zero;

    Vector3 LastPos;

    bool Fire = true;

    Vector2 StartPress = Vector2.zero;
    Vector2 EndPress = Vector2.zero;

    // Find stuff
    private void Start()
    {
        targetRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            LastPos = LastPos == targetRB.gameObject.transform.position ? LastPos : targetRB.gameObject.transform.position;
            print(LastPos);
        }
    }

    // Pivate
    void _AddVel(float force)
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
            targetRB.AddForce(new Vector3(horizontalDirection.x, 0, horizontalDirection.y) * force, ForceMode.Impulse);
        }
    }

    // Toggle when held and released
    void OnFire(InputValue Value)
    {
        if (Fire)
        {
            StartPress = Mouse.current.position.ReadValue();
        }
        else
        {
            EndPress = Mouse.current.position.ReadValue();
            Vector2 Total = EndPress - StartPress;

            _AddVel(Total.magnitude);
        }
        Fire = !Fire;
    }

    void OnFire2()
    {
        targetRB.velocity = Vector3.zero;
        targetRB.angularDrag = 0f;
        targetRB.angularVelocity = Vector3.zero;
    }

    void OnReset()
    {
        OnFire2();
        transform.position = LastPos;
    }

}