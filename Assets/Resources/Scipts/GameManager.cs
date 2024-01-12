using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EventSystem;
    PauseMenuScriptHvisNavnetErTagetErJegFucked pms;

    GameObject Player;
    Transform[] Holes;
    int CurrentHole = 0;

    private void Awake()
    {
        Player = Resources.Load<GameObject>(@"Prefabs/Player/Ball");

        pms = EventSystem.GetComponent<PauseMenuScriptHvisNavnetErTagetErJegFucked>();

        Holes = GameObject.FindGameObjectsWithTag("Hole").Select(x => x.transform).ToArray();
    }

    private void Start()
    {
        for (int i = 0; i < pms.playerNumbers; i++)
        {
            GameObject temp = Instantiate(Player, Holes[i]);
            Camera.main.GetComponent<CameraControl>().targets.Add(temp.transform);
        }
        Camera.main.SendMessage("Begin");
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
