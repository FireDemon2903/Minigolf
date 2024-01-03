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
    public TextMeshProUGUI titleText;
    [SerializeField] public bool is_paused = false;
    bool mom_i_am_winning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void vicMenu()
    {
        mom_i_am_winning = true;
        Time.timeScale = 0;
        vicMenuen.SetActive(true);
        int dims = SceneManager.GetActiveScene().buildIndex;
        titleText.text = $"Hole {dims} Done";
    }

    public void nextHole()
    {
        mom_i_am_winning = false;
        Time.timeScale = 1;
        vicMenuen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

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

    public void Quit()
    {
        Application.Quit();
    }
}
