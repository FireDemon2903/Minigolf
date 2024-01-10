using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    Transform[] Holes;
    int CurrentHole = 0;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");

        Holes = GameObject.FindGameObjectsWithTag("Hole").Select(x => x.transform).ToArray();
    }

    private void Update()
    {
        // For testing; not important
        if (Input.GetKeyDown(KeyCode.G))
        {
            NextHole();
        }
    }

    void NextHole()
    {
        ToHole(CurrentHole);
        CurrentHole++;
    }

    void ToHole(int hole)
    {
        Player.transform.position = Holes[hole].position;
    }

    void Quit() { Application.Quit(); }
}
