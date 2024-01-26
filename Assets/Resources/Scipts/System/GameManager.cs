using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class GameManager : MonoBehaviour
{
    public GameObject EventSystem;
    PauseMenuScriptHvisNavnetErTagetErJegFucked pms;

    CameraControl cameraControl;

    GameObject PlayerPrefab;
    readonly List<GameObject> Players = new();
    [SerializeField] List<Transform> StartingPositions;
    int CurrentHole = 0;

    public readonly List<Vector3> Scaling = new()
    {
        new Vector3(.1f, .1f, .1f) * 2,
        Vector3.one,
        new Vector3(.5f, .5f, .5f),
        new Vector3()
    };
    readonly List<Color> colors = new()
    {
        Color.red,
        Color.green,
        Color.blue,
        new Color(218,165,32)
    };

    private void Start()
    {
        PlayerPrefab = Resources.Load<GameObject>(@"Prefabs/Player/PlayerPrefab");

        cameraControl = Camera.main.GetComponent<CameraControl>();
        pms = EventSystem.GetComponent<PauseMenuScriptHvisNavnetErTagetErJegFucked>();

        for (int i = 0; i < pms.playerNumbers; i++)
        {
            GameObject temp = Instantiate(PlayerPrefab, StartingPositions[0].position, Quaternion.identity);
            temp.GetComponent<PlayerControls>().gameManager = this;
            cameraControl.targets.Add(temp.transform);
            Players.Add(temp);
            temp.GetComponent<Renderer>().material.color = colors[i];
            ScaleAll(temp, 0);
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
            ToHole(cameraControl.targetObject.gameObject, CurrentHole);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleBuiltinGravity(cameraControl.targetObject.gameObject);
        }
    }

    public void UpdateScore(GameObject player, int hits)
    {
        int i = Players.IndexOf(player);
        pms.updateScoreborad(CurrentHole, hits, i);
    }

    public void ToHole(GameObject player, int holeIndex)
    {
        // If the player reaches last level (planets)
        if (holeIndex == StartingPositions.Count - 1) { ToggleBuiltinGravity(player); }
        ScaleAll(player, holeIndex);
        player.transform.position = StartingPositions[holeIndex].position;
    }
    
    void ToggleBuiltinGravity(GameObject player)
    {
        player.GetComponent<Rigidbody>().useGravity = !player.GetComponent<Rigidbody>().useGravity;
    }

    void ScaleAll(GameObject player, int holeIndex)
    {
        player.GetComponent<Rigidbody>().mass = Scaling[holeIndex].x * 10;
        player.transform.localScale = Scaling[holeIndex];
    }

    public void PlayerWon(GameObject player)
    {
        cameraControl.SendMessage("NextBall");
        cameraControl.targets.Remove(player.transform);
        Destroy(player);
    }

    void Quit() { Application.Quit(); }
}
