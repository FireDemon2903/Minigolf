using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTheInTheHoleThing : MonoBehaviour
{
    GameObject TheHole;
    public PauseMenuScriptHvisNavnetErTagetErJegFucked psme;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            psme.vicMenu();
            int dims = Random.Range(1, 40); // fjern n�r det er
            psme.holesPassed++;
            //psme.updateScoreborad(psme.holesPassed, dims);
        }
    }
}
