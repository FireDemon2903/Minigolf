using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(transform.position, transform.up, 80 * Time.deltaTime);
    }
}
