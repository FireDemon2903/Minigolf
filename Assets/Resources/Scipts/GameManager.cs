using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Transform[] StartPlace;
    Transform Holes;


    private void Start()
    {
        StartPlace = GameObject.FindGameObjectsWithTag("Hole").Select(x => x.transform).ToArray();
        print(StartPlace.Length);
    }
}
