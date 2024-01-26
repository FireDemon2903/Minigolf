using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CustomPhysicsEngine : MonoBehaviour
{
    public static List<GravitySource> sources = new();

    private void Start()
    {
        sources.AddRange(GameObject.FindGameObjectsWithTag("Planet").Select(x => x.GetComponent<GravitySphere>()));
        //Physics.gravity = Vector3.zero;
        print(sources.Count);
    }

    public static Vector3 GetTotalGravity(Vector3 position)
    {
        Vector3 gravity = Vector3.zero;
        foreach (var source in sources)
        {
            gravity += source.GetGravity(position);
        }
        return gravity;
    }
}
