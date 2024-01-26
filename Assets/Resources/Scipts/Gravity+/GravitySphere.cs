using System;
using UnityEngine;

public class GravitySphere : GravitySource
{
    [SerializeField, Min(0f)]
    float gravity = 30f; //9.81f;

    [SerializeField, Min(0f)]
    float outerRadius = 10f, outerFalloffRadius = 15f;

    private void Update()
    {
        transform.position.DrawSphere(outerRadius, Color.green);
        transform.position.DrawSphere(outerFalloffRadius, Color.yellow);
    }

    public override Vector3 GetGravity(Vector3 position)
    {
        Vector3 direction = transform.position - position;
        float distance = direction.magnitude;
        if (distance <= outerRadius)
        {
            return direction.normalized * gravity;
        }
        else if (distance <= outerFalloffRadius)
        {
            float falloff = (distance - outerRadius) / (outerFalloffRadius - outerRadius);
            return (1 - falloff) * gravity * direction.normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }

}

