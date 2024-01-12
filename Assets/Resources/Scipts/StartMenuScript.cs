using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] GameObject petergriffin;
    [SerializeField] TMP_Dropdown dophigh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        print(dophigh.options[dophigh.value].text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void controlsGuide()
    {
        petergriffin.SetActive(true);
    }

    public void closeDims()
    {
        petergriffin.SetActive(false);
    }

    public void doTheQuitting()
    {
        Application.Quit();
    }
}
