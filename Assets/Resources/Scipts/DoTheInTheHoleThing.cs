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
        }
    }
}
