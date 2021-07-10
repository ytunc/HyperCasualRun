using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static GameManager instance = null;

    // Game Instance Singleton
    public static GameManager Instance
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
        BaseStart();
    }
    #endregion

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private FloorObjectPools floorObjectPools;
    [SerializeField] private PrizeAmdPunishmentPool prizeAmdPunishmentPool;
    [SerializeField] private Player player;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private TotalNumberObject totalNumberObject;

    public void BaseStart()
    {
        cameraController = FindObjectOfType<CameraController>();
        floorObjectPools = FindObjectOfType<FloorObjectPools>();
        prizeAmdPunishmentPool = FindObjectOfType<PrizeAmdPunishmentPool>();
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UiManager>();
        energyBar = FindObjectOfType<EnergyBar>();
        totalNumberObject = FindObjectOfType<TotalNumberObject>();

        floorObjectPools.FirstIns(10);
        prizeAmdPunishmentPool.Start();
        prizeAmdPunishmentPool.StartCoroutine(nameof(prizeAmdPunishmentPool.Spawner));
        player.StartGame();
        uiManager.StartGame();
        energyBar.SetEnergy(30);
        totalNumberObject.SetTotalNumberObject(0);


        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayMusic();
    }
}
