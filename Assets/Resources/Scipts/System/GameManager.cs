using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // :)

    public GameObject EventSystem;
    PauseMenuScriptHvisNavnetErTagetErJegFucked pms;

    CameraControl cameraControl;

    GameObject PlayerPrefab;
    List<GameObject> Players = new();
    [SerializeField] List<Transform> StartingPositions;
    int CurrentHole = 0;

    private void Start()
    {
        PlayerPrefab = Resources.Load<GameObject>(@"Prefabs/Player/PlayerPrefab");

        cameraControl = Camera.main.GetComponent<CameraControl>();
        pms = EventSystem.GetComponent<PauseMenuScriptHvisNavnetErTagetErJegFucked>();

        for (int i = 0; i < pms.playerNumbers; i++)
        {
            GameObject temp = Instantiate(PlayerPrefab, StartingPositions[0]);
            temp.GetComponent<PlayerControls>().gameManager = this;
            cameraControl.targets.Add(temp.transform);
            Players.Add(temp);
        }
        Camera.main.SendMessage("Begin");
    }

    private void Awake()
    {
        StartingPositions = GameObject.FindGameObjectsWithTag("Startpoint").Select(x => x.transform).OrderBy(x => x.position.x).ToList();

        print(StartingPositions.Count);
    }

    private void Update()
    {
        // For testing; not important
        if (Input.GetKeyDown(KeyCode.G))
        {
            NextHole(cameraControl.targetObject.gameObject, CurrentHole);
        }
    }

    public void UpdateScore(GameObject player, int hits)
    {
        int i = Players.IndexOf(player);
        pms.updateScoreborad(CurrentHole, hits, i);
    }

    public void NextHole(GameObject player, int holeIndex)
    {
        player.transform.position = StartingPositions[holeIndex].position;
    }

    void Quit() { Application.Quit(); }
}
