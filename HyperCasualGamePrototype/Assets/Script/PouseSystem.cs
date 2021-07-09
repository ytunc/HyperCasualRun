using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PouseSystem : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static PouseSystem instance = null;

    // Game Instance Singleton
    public static PouseSystem Instance
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
    [SerializeField] private TextMeshProUGUI pouseButtonText;
    [SerializeField] private bool pouse;

    public void PouseGame()
    {
        pouse = !pouse;

        if (pouse == false)
        {
            Time.timeScale = 0;
            pouseButtonText.text = "Stop";
        }
        else if (pouse == true)
        {
            Time.timeScale = 1;
            pouseButtonText.text = "start";
        }
    }
}
