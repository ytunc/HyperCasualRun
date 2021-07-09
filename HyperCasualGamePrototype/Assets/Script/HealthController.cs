using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static HealthController instance = null;

    // Game Instance Singleton
    public static HealthController Instance
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

    [SerializeField] private Color32 white, black;
    [SerializeField] private Image[] images;
    [SerializeField] private int healthCount = 0;


    private void Start()
    {
        CheckHealthCount();
    }


    public void CheckHealthCount()
    {
        healthCount = PlayerPrefs.GetInt("health");
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = black;
        }

        for (int i = 0; i < PlayerPrefs.GetInt("health"); i++)
        {
            images[i].color = white;
        }
    }
}
