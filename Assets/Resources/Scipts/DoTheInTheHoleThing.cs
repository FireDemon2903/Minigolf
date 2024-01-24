using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTheInTheHoleThing : MonoBehaviour
{
    GameObject TheHole;
    public PauseMenuScriptHvisNavnetErTagetErJegFucked psme;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            psme.vicMenu();
            int dims = Random.Range(1, 40); // fjern når det er
            psme.updateScoreborad(1, dims + 1, 1);
        }
    }
}
