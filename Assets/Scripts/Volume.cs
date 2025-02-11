using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider volumeSlider;
    public Text volumeText;
    
    
    void Start()
    {
        UpdateSliderText(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(UpdateSliderText);
    }
    
    public void UpdateSliderText(float value)
    {
        volumeText.text = Mathf.RoundToInt(value).ToString() + "%";
    }


}
