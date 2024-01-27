using UnityEngine;

/// <summary>
/// A scource of gravity
/// </summary>
public class GravitySource : MonoBehaviour
{
    public virtual Vector3 GetGravity(Vector3 position)
    {
        return Physics.gravity;
    }
}
