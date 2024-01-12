using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float forceToAdd = 1f;

    Rigidbody targetRB;
    Vector3 TargetVelocity => targetRB.velocity;
    bool IsMoving => TargetVelocity != Vector3.zero;        // True if target vel is not zero

    Vector3 LastPos;

    // True is lmb is held down
    bool LMBPressed = true;

    // true after force has been added, toggles affter coroutine to change player was started
    bool fired = false;

    Vector2 StartPress = Vector2.zero;
    Vector2 EndPress = Vector2.zero;

    // Find stuff
    private void Start()
    {
        targetRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsMoving)
        {
            LastPos = LastPos == targetRB.gameObject.transform.position ? LastPos : targetRB.gameObject.transform.position;
            print(LastPos);
        }

        if (fired && IsMoving)
        {
            fired = false;
            StartCoroutine(WaitForMove());
        }
    }

    // Pivate
    void _AddVel(float force)
    {
        if (IsMoving)
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

            fired = true;
        }
    }

    // Toggle when held and released
    void OnFire()
    {
        if (LMBPressed)
        {
            StartPress = Mouse.current.position.ReadValue();
        }
        else
        {
            EndPress = Mouse.current.position.ReadValue();
            Vector2 total = EndPress - StartPress;

            // To make sure thee ae no missclicks
            if (total.magnitude > 50)
                _AddVel(total.magnitude);
        }
        LMBPressed = !LMBPressed;
    }

    void OnFire2()
    {
        targetRB.velocity = Vector3.zero;
        targetRB.angularDrag = 0f;
        targetRB.angularVelocity = Vector3.zero;
    }

    // Resets to last pos where ball was not moveing (then changes player (unintuitive, but oh well))
    void OnReset()
    {
        OnFire2();
        transform.position = LastPos;
    }

    // Wait for ball to stop moving, then change player
    IEnumerator WaitForMove()
    {
        // Wait
        while (IsMoving)
        {
            yield return null;
        }

        // Finished wait
        print("Message sent"); Camera.main.SendMessage("NextBall");
    }

}