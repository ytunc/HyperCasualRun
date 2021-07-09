using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalNumberObject : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static TotalNumberObject instance = null;

    // Game Instance Singleton
    public static TotalNumberObject Instance
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
    [SerializeField] private TextMeshProUGUI totalNumberObjectText;
    [SerializeField] private int count;

    public void SetTotalNumberObject(int value)
    {
        count += value;

        totalNumberObjectText.text = "Total Object Count: " + count.ToString();

    }

}
