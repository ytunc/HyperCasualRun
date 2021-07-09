using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static EnergyBar instance = null;

    // Game Instance Singleton
    public static EnergyBar Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI energyPowerText;
    [SerializeField] private int energyValue;

    public void SetEnergy(int value)
    {
        energyValue += value;
        energySlider.value = energyValue;
        energyPowerText.text = "% " + energyValue.ToString();  
    }

}
