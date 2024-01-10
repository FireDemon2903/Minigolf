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
    public GameObject settingmenu;
    public TextMeshProUGUI titleText;

    public bool is_paused = false;
    public int holesPassed = 0;
    bool mom_i_am_winning = false;
    [SerializeField] int numberOfHoles = 5;
    [SerializeField] int[] holePoints;

    public GameObject ThePrefrabOfTheHolesYeah;
    public GameObject TheParentOfAllParents;
    [SerializeField] List<List<GameObject>> TheHolesInScoreboard = new List<List<GameObject>>(); // TheHolesInScoreboard[0][0] = Hul tekst, TheHolesInScoreboard[0][1] = point tekst, TheHolesInScoreboard[0][3] objektet som holder p� dem
    // Start is called before the first frame update
    void Start()
    {
        is_paused = false;
        // laver de er ting hvor man f�r et overblik over hvilke huller som der er, og hvor mange points man har i alt.
        for (int i = 0; i < numberOfHoles; i++)
        {
            GameObject kurt = Instantiate(ThePrefrabOfTheHolesYeah, new Vector3(TheParentOfAllParents.transform.position.x - 273 + (i * 50), TheParentOfAllParents.transform.position.y - 1, TheParentOfAllParents.transform.position.z), Quaternion.identity);
            //kurt.transform.parent = TheParentOfAllParents.transform;
            kurt.transform.SetParent(TheParentOfAllParents.transform);

            // f� b�rneneenenen af hovedobjektet, for det skal man �benbart have hvis man gerne vil �ndre p� det der tekst.
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

    #region settingsMenuen
    public void openSetMenu()
    {
        settingmenu.SetActive(true);
        pauseMenuen.SetActive(false);
    }

    public void closeSetMenu()
    {
        settingmenu.SetActive(false);
        pauseMenuen.SetActive(true);
    }
    #endregion settingsMenuen

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
    }
    #endregion WinMenu

    #region Scoreboard
    /// <summary>
    /// opdatere hullets slag hvis du forst�r
    /// </summary>
    /// <param name="hole">Hvilket hul som kan have sine points opdateret, her skal du bruge hul nummeret, s� det  f�rste hulnummer er 1</param>
    /// <param name="hits">det antal som det har taget at klare det sepcifikket hul. stavede jeg speciffffikke rigtigt</param>
    public void updateScoreborad(int hole, int hits)
    {
        TheHolesInScoreboard[hole - 1][1].GetComponent<TextMeshProUGUI>().text = $"{hits}";
    }

    #endregion Scoreboard


    #region Controls

    // ESC button pressed
    void OnThePauseActionTakeingDims(InputValue value)
    {
        print("Jesus");
        if (!is_paused && !mom_i_am_winning)
        {
            pauseDims();
        }
        else if (!mom_i_am_winning && is_paused)
        {
            unPauseDims();
            settingmenu.SetActive(false);
        }
    }

    // Tab button pressed and so on
    void OnTheScoreboardActionDims(InputValue value)
    {
        scoreborad.SetActive(!scoreborad.activeSelf);
    }

    #endregion Controls

    public void Quit()
    {
        Application.Quit();
    }
}
