using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScriptHvisNavnetErTagetErJegFucked : MonoBehaviour
{
    public GameObject pauseMenuen;
    public bool is_paused = false;
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
        Time.timeScale = 0;
    }

    public void unPauseDims()
    {
        Time.timeScale = 1;
        pauseMenuen.SetActive(is_paused);
    }
}
