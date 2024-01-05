using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuScriptHvisNavnetErTagetErJegFucked : MonoBehaviour
{
    public GameObject pauseMenuen;
    public GameObject vicMenuen;
    public GameObject scoreborad;
    public TextMeshProUGUI titleText;

    public bool is_paused = false;
    public int holesPassed = 0;
    bool mom_i_am_winning = false;
    [SerializeField] int numberOfHoles = 5;
    [SerializeField] int[] holePoints;

    public GameObject ThePrefrabOfTheHolesYeah;
    public GameObject TheParentOfAllParents;
    [SerializeField] List<List<GameObject>> TheHolesInScoreboard = new List<List<GameObject>>(); // TheHolesInScoreboard[0][0] = Hul tekst, TheHolesInScoreboard[0][1] = point tekst, TheHolesInScoreboard[0][3] objektet som holder på dem
    // Start is called before the first frame update
    void Start()
    {
        // laver de er ting hvor man får et overblik over hvilke huller som der er, og hvor mange points man har i alt.
        for (int i = 0; i < numberOfHoles; i++)
        {
            GameObject kurt = Instantiate(ThePrefrabOfTheHolesYeah, new Vector3(TheParentOfAllParents.transform.position.x - 273 + (i * 50), TheParentOfAllParents.transform.position.y - 1, TheParentOfAllParents.transform.position.z), Quaternion.identity);
            kurt.transform.parent = TheParentOfAllParents.transform;

            // få børneneenenen af hovedobjektet, for det skal man åbenbart have hvis man gerne vil ændre på det der tekst.
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in kurt.transform)
            {
                children.Add(child.gameObject);
            }
            children.Add(kurt);
            TheHolesInScoreboard.Add(children);

            TextMeshProUGUI peter = TheHolesInScoreboard[i][0].GetComponent<TextMeshProUGUI>();
            peter.text = $"{i + 1}";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Pausemenu

    public void pauseDims()
    {
        is_paused = true;
        pauseMenuen.SetActive(is_paused);
        Time.timeScale = 0;
    }

    public void unPauseDims()
    {
        is_paused = false;
        Time.timeScale = 1;
        pauseMenuen.SetActive(is_paused);
    }

    #endregion Pausemenu

    #region WinMenu
    public void vicMenu()
    {
        mom_i_am_winning = true;
        Time.timeScale = 0;
        vicMenuen.SetActive(true);
        //int dims = SceneManager.GetActiveScene().buildIndex;
        holesPassed++;
        titleText.text = $"Hole {holesPassed} Done";
    }

    public void nextHole()
    {
        mom_i_am_winning = false;
        Time.timeScale = 1;
        vicMenuen.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //go to the next hole function
    }
    #endregion WinMenu

    #region Scoreboard

    #endregion Scoreboard


    #region Controls

    // ESC button pressed
    void OnThePauseActionTakeingDims(InputValue value)
    {
        if (!is_paused && !mom_i_am_winning)
        {
            pauseDims();
        }
        else if (!mom_i_am_winning && is_paused)
        {
            unPauseDims();
        }
    }

    // Tab button pressed and so on
    void OnTheScoreboardActionDims(InputValue value)
    {
        scoreborad.SetActive(!scoreborad.active);
    }

    #endregion Controls

    public void Quit()
    {
        Application.Quit();
    }
}
