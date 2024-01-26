using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LydHondteringsScript : MonoBehaviour
{
    public TextMeshProUGUI volumeLabel;
    public Slider slider;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = slider.value;
        volumeLabel.text = $"Lydstyrke: {Mathf.Round(volume * 100f)}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dasSliderValchange()
    {
        volume = slider.value;

        volumeLabel.text = $"Lydstyrke: {Mathf.Round(volume * 100f)}";
    }
}
