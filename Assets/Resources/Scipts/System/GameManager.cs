using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EventSystem;                              // Used to get PMS
    PauseMenuScriptHvisNavnetErTagetErJegFucked pms;            // Pause menu (and other menues) ref

    CameraControl cameraControl;                                // Cam control script

    GameObject PlayerPrefab;                                    // Player prefab
    readonly List<GameObject> Players = new();                  // List of players
    [SerializeField] List<Transform> StartingPositions;         // Startingpositions of the levels

    // Level scaling
    public readonly List<Vector3> Scaling = new()
    {
        new Vector3(.2f, .2f, .2f),
        Vector3.one,
        new Vector3(.5f, .5f, .5f),
        Vector3.one,
        Vector3.one,
        new Vector3(.5f, .5f, .5f),
        Vector3.one
    };
    // Player colours
    readonly List<Color> colors = new()
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow  //new Color(218,165,32)  // Golden
    };

    private void Start()
    {
        PlayerPrefab = Resources.Load<GameObject>(@"Prefabs/Player/PlayerPrefab");

        cameraControl = Camera.main.GetComponent<CameraControl>();
        pms = EventSystem.GetComponent<PauseMenuScriptHvisNavnetErTagetErJegFucked>();

        for (int i = 0; i < pms.playerNumbers; i++)
        {
            GameObject temp = Instantiate(PlayerPrefab, StartingPositions[0].position + new Vector3(.5f * i, 0, 0), Quaternion.identity);
            temp.GetComponent<PlayerControls>().gameManager = this;
            cameraControl.targets.Add(temp.transform);
            Players.Add(temp);
            temp.GetComponent<Renderer>().material.color = colors[i];
            temp.GetComponent<Light>().color = colors[i];
            ScaleAll(temp, 0);
        }
        Camera.main.SendMessage("Begin");
    }

    private void Awake()
    {
        StartingPositions = GameObject.FindGameObjectsWithTag("Startpoint").Select(x => x.transform).OrderBy(x => x.position.x).ToList();
    }

    // Updates the scoreboard for the player
    public void UpdateScore(GameObject player, int hits)
    {
        int i = Players.IndexOf(player);
        pms.updateScoreborad(player.GetComponent<PlayerControls>().Hole, hits, i);
    }

    // Teleports player to hole
    public void ToHole(GameObject player, int holeIndex)
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;                               // Reset vel
        if (holeIndex == 5) { ToggleBuiltinGravity(player); print("toggle"); }                  // If the player reaches last level (planets)
        ScaleAll(player, holeIndex);                                                            // Scale
        player.transform.position = StartingPositions[holeIndex].position;                      // Set pos
    }
    
    // Used for last level
    void ToggleBuiltinGravity(GameObject player)
    {
        player.GetComponent<Rigidbody>().useGravity = !player.GetComponent<Rigidbody>().useGravity;
    }

    // Scales the player object
    void ScaleAll(GameObject player, int holeIndex)
    {
        player.GetComponent<Rigidbody>().mass = Scaling[holeIndex].x * 10;
        player.transform.localScale = Scaling[holeIndex];
    }

    // Player reached the final moon/local sattelite
    public void PlayerWon(GameObject player)
    {
        cameraControl.SendMessage("NextBall");
        cameraControl.targets.Remove(player.transform);
    }

    void Quit() { Application.Quit(); }
}
