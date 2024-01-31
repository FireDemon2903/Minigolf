using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public GameManager gameManager;                         // GM ref

    GravitySphere[] gravitySources;                         // List of gravity scources to check in final level

    public int Hits = 0;                                    // Hits on current hole
    public int Hole = 0;                                    // Current hole

    Rigidbody targetRB;
    Vector3 TargetVelocity => targetRB.velocity;
    bool IsMoving => TargetVelocity.magnitude > 5;          // True if target vel is not zero

    Vector3 LastPos;                                        // Last position where IsMoving was false

    // True is lmb is held down
    bool LMBPressed = true;

    // true after force has been added, toggles affter coroutine to change player was started
    bool fired = false;

    // Used to calc LastPos
    Vector2 StartPress = Vector2.zero;
    Vector2 EndPress = Vector2.zero;

    // Find stuff
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        targetRB = GetComponent<Rigidbody>();
        gravitySources = FindObjectsOfType<GravitySphere>();
        //targetRB.useGravity = false; Hole = 6;      // For debugging last level
    }

    // Custom gravity for final level
    private void FixedUpdate()
    {
        Vector3 gravity = Vector3.zero;
        foreach (var source in gravitySources)
        {
            gravity += source.GetGravity(targetRB.position);
        }
        targetRB.velocity += gravity * Time.fixedDeltaTime;

    }

    private void Update()
    {
        if (!IsMoving)
        {
            // Calc last position
            LastPos = LastPos == targetRB.gameObject.transform.position ? LastPos : targetRB.gameObject.transform.position;
        }

        if (fired && IsMoving)
        {
            // Player fired, stop listening
            fired = false;
            StartCoroutine(WaitForMove());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            Hole++;
            Hits = 0;
            gameManager.ToHole(gameObject, Hole);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            print(gameObject.name);
            gameManager.PlayerWon(gameObject);
        }
        else if (other.CompareTag("Saturn"))
        {
            gameManager.ToHole(gameObject, Hole);
        }
    }

    // Pivate
    void AddVel(float force)
    {
        if (IsMoving)
        {
            print("ball is moving, cannot fire");
        }
        else
        {
            // Find direction
            Vector3 direction = targetRB.transform.position - Camera.main.transform.position;

            // Scale the force
            force *= gameManager.Scaling[Hole].x * .2f;

            // Add the force
            if (Hole != 5)
            {
                // Find horizontal direction and normalize.
                Vector2 horizontalDirection = new Vector2(direction.x, direction.z).normalized;
                
                targetRB.AddForce(new Vector3(horizontalDirection.x, 0, horizontalDirection.y) * force, ForceMode.Impulse);
            }
            else
            {
                // Here the direction is not horizontal only, as it is planet level
                targetRB.AddForce(direction.normalized * force * 2, ForceMode.Impulse);
            }

            fired = true;

            Hits++;

            gameManager.UpdateScore(gameObject, Hits);
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

            // the total distence mouse moved when lmb was held
            Vector2 total = EndPress - StartPress;

            // To make sure thee ae no missclicks
            if (total.magnitude > 50) { AddVel(total.magnitude); }
            else { LMBPressed = !LMBPressed; }
        }
        LMBPressed = !LMBPressed;
    }

    // For debugging (button is 'e')
    void OnFire2()
    {
        targetRB.velocity = Vector3.zero;
        targetRB.angularVelocity = Vector3.zero;
    }

    // Resets to last pos where ball was not moveing (then changes player (unintuitive, but oh well))
    void OnReset()
    {
        Hits++;
        OnFire2();
        transform.position = LastPos;
    }

    // Back to start of hole
    void OnHardReset()
    {
        Hits++;
        gameManager.ToHole(gameObject, Hole);
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