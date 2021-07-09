using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SoundManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static SoundManager instance = null;

    // Game Instance Singleton
    public static SoundManager Instance
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

    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string status;

    public void PlayMusic()
    {
        if (PlayerPrefs.GetString("voice") == "true")
        {
            audioSource.volume = 1;
            return;
        }
        else if (PlayerPrefs.GetString("voice") == "false")
        {
            audioSource.volume = 0;
            return;
        }
    }
    private void Update()
    {
        status = PlayerPrefs.GetString("voice");
    }

    public void VolumeChange()
    {
        if (PlayerPrefs.GetString("voice") == "true" )
        {
            PlayerPrefs.SetString("voice", "false");
            volumeText.text = PlayerPrefs.GetString("voice");
            return;
        }
        else if (PlayerPrefs.GetString("voice") == "false")
        {
            PlayerPrefs.SetString("voice", "true");
            volumeText.text = PlayerPrefs.GetString("voice");
            return;
        }
        else if (PlayerPrefs.GetString("voice") != "true" && PlayerPrefs.GetString("voice") != "false")
        {
            PlayerPrefs.SetString("voice", "true");
        }
    }
}
