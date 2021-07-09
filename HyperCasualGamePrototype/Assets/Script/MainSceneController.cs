using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MainSceneController : MonoBehaviour
{

    #region SINGLETON PATTERN
    private static MainSceneController instance = null;

    // Game Instance Singleton
    public static MainSceneController Instance
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

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject rewardAds;

    [SerializeField] private HealthController healthController;

    [SerializeField] private bool daily;
    [SerializeField] private TextMeshProUGUI dailyReward;

    public enum panels
    {
        mainPanel, settingPanel,rewardAds
    }

    private void Start()
    {
        ChangePanel(panels.mainPanel);
        if (PlayerPrefs.GetInt("year") == 0)
        {
            GetTime();
        }
    }

    public void GetTime()
    {
        PlayerPrefs.SetInt("year", DateTime.Now.Year);
        PlayerPrefs.SetInt("month", DateTime.Now.Month);
        PlayerPrefs.SetInt("day", DateTime.Now.Day);
        PlayerPrefs.SetInt("hour", DateTime.Now.Hour);
        Debug.Log(PlayerPrefs.GetInt("year"));
    }

    public void TakeItReward()
    {
        if (DateTime.Now.Year == PlayerPrefs.GetInt("year") &&
            DateTime.Now.Year == PlayerPrefs.GetInt("month") &&
            DateTime.Now.Year >= PlayerPrefs.GetInt("day") &&
            DateTime.Now.Year >= PlayerPrefs.GetInt("hour"))
        {
            PlayerPrefs.SetInt("health", 3);
            healthController.CheckHealthCount();
            GetTime();
            dailyReward.text = "Congratulations";
        }
        else
        {
            dailyReward.text = "The time is not came";
        }
    }

    public void ChangePanel(panels panelName)
    {
        settingPanel.SetActive(false);
        mainPanel.SetActive(false);
        rewardAds.SetActive(false);

        switch (panelName)
        {
            case panels.mainPanel:
                mainPanel.SetActive(true);
                break;
            case panels.settingPanel:
                settingPanel.SetActive(true);
                break;
            case panels.rewardAds:
                rewardAds.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SettingButton()
    {
        ChangePanel(panels.settingPanel);
    }

    public void BackButton()
    {
        ChangePanel(panels.mainPanel);
    }

    public void PlayButton()
    {
        if (PlayerPrefs.GetInt("health") > 0)
        {
            PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") - 1);
            Debug.Log(PlayerPrefs.GetInt("health").ToString());
            SceneManager.LoadScene(1);
        }
        else
        {
            ChangePanel(panels.rewardAds);
        }
        healthController.CheckHealthCount();
    }

}
