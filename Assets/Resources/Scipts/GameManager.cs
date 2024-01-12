using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EventSystem;
    PauseMenuScriptHvisNavnetErTagetErJegFucked pms;

    GameObject PlayerPrefab;
    List<GameObject> Players = new();
    Transform[] Holes;
    int CurrentHole = 0;

    private void Awake()
    {
        PlayerPrefab = Resources.Load<GameObject>(@"Prefabs/Player/Ball");

        pms = EventSystem.GetComponent<PauseMenuScriptHvisNavnetErTagetErJegFucked>();

        Holes = GameObject.FindGameObjectsWithTag("Hole").Select(x => x.transform).ToArray();
    }

    private void Start()
    {
        for (int i = 0; i < pms.playerNumbers; i++)
        {
            GameObject temp = Instantiate(PlayerPrefab, Holes[i]);
            temp.gameObject.GetComponent<PlayerControls>().gameManager = this;
            Camera.main.GetComponent<CameraControl>().targets.Add(temp.transform);
            Players.Add(temp);
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

    public void UpdateScore(GameObject player, int hits)
    {
        int i = Players.IndexOf(player);
        pms.updateScoreborad(CurrentHole, hits, i);
    }

    void NextHole()
    {
        ToHole(CurrentHole);
        CurrentHole++;
    }

    void ToHole(int hole)
    {
        PlayerPrefab.transform.position = Holes[hole].position;
    }

    void Quit() { Application.Quit(); }
}
