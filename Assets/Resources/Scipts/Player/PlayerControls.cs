using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public GameManager gameManager;

    GravitySource[] gravitySources;

    public int Hits = 0;
    public int Hole = 0;

    Rigidbody targetRB;
    Vector3 TargetVelocity => targetRB.velocity;
    bool IsMoving => TargetVelocity.magnitude > 5;        // True if target vel is not zero

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
        gameManager = FindAnyObjectByType<GameManager>();
        targetRB = GetComponent<Rigidbody>();
        gravitySources = FindObjectsOfType<GravitySource>();
        targetRB.useGravity = false; Hole = 6;
    }

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
            LastPos = LastPos == targetRB.gameObject.transform.position ? LastPos : targetRB.gameObject.transform.position;
        }

        if (fired && IsMoving)
        {
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
            if (Hole != 6)
            {
                // Find horizontal direction and normalize
                Vector2 horizontalDirection = new Vector2(direction.x, direction.z).normalized;
                
                targetRB.AddForce(new Vector3(horizontalDirection.x, 0, horizontalDirection.y) * force, ForceMode.Impulse);
            }
            else
            {
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
            Vector2 total = EndPress - StartPress;

            // To make sure thee ae no missclicks
            if (total.magnitude > 50) { AddVel(total.magnitude); } else { LMBPressed = !LMBPressed; }
        }
        LMBPressed = !LMBPressed;
    }

    void OnFire2()
    {
        targetRB.velocity = Vector3.zero;
        targetRB.angularVelocity = Vector3.zero;
    }

    // Resets to last pos where ball was not moveing (then changes player (unintuitive, but oh well))
    void OnReset()
    {
        OnFire2();
        transform.position = LastPos;
    }

    void OnHardReset()
    {
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