using UnityEngine;

/// <summary>
/// Class for drawing circles around gameobjects in the editor.
/// </summary>
public class DrawMe : MonoBehaviour
{
    [SerializeField, ColorUsage(true, order = 1)] Color m_Color = Color.white;
    [SerializeField, Min(.1f)] float radius = .5f;
    void Update() { gameObject.transform.position.DrawSphere(radius, m_Color); }
}
