using UnityEngine;

/// <summary>
/// A spherical source of gravity. Used in planet level
/// </summary>
public class GravitySphere : GravitySource
{
    // Gravity force
    [SerializeField, Min(0f)]
    float gravity = 30f; //9.81f;

    // outer radius is the area where gravity is constant
    // outer falloff is when gravity starts to get weaker
    [SerializeField, Min(0f)]
    float outerRadius = 10f, outerFalloffRadius = 15f;

    // For edditor debugging, to see where they overlap
    private void Update()
    {
        transform.position.DrawSphere(outerRadius, Color.green);
        transform.position.DrawSphere(outerFalloffRadius, Color.yellow);
    }

    // Used to get the gravity of this object
    public override Vector3 GetGravity(Vector3 position)
    {
        // Get the direction of the force
        Vector3 direction = transform.position - position;

        // Get the distance between this and other object(player)
        float distance = direction.magnitude;

        // If the player is close to this object, it will get all of the gravity
        if (distance <= outerRadius)
        {
            return direction.normalized * gravity;
        }
        // If the player is further away, it will get less gravity
        else if (distance <= outerFalloffRadius)
        {
            float falloff = (distance - outerRadius) / (outerFalloffRadius - outerRadius);
            return (1 - falloff) * gravity * direction.normalized;
        }
        // If the player is far away, it is not affected by this object
        else
        {
            return Vector3.zero;
        }
    }

}

