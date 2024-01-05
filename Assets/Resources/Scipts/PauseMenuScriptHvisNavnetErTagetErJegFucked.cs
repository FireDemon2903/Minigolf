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

    [SerializeField] GameObject ThePrefrabOfTheHolesYeah;
    [SerializeField] GameObject TheParentOfAllParents;
    [SerializeField] List<GameObject> TheHolesInScoreboard = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfHoles; i++)
        {
            GameObject kurt = Instantiate(ThePrefrabOfTheHolesYeah, new Vector3(0, 0, 0), Quaternion.identity);
            kurt.transform.parent = TheParentOfAllParents.transform;
            TheHolesInScoreboard.Add(kurt);
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
