using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _enabledTurretSpawn = 0;
    private int _enabledPickupSpawn = 0;

    [SerializeField] private CarController[] _cars;

    private Gun[] _playerGuns;

    private CarController player;
    private AI[] enemies;
    private List<AI> enemiesList = new List<AI>();

    private PickupSpawner[] _pickupSpawners;

    private TurretSpawn[] _turretSpawner;

    private bool? hasPlayerWon = null;

    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    public int _enemyDeathCount;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        foreach(var car in _cars)
        {
            if(car.player.carID == SelectedCarInfo.Instance.CarID)
            {
                car.gameObject.SetActive(true);
                _playerGuns = car.GetComponentsInChildren<Gun>();
            }
            else
                car.gameObject.SetActive(false);
        }

        foreach(var gun in _playerGuns)
        {
            if (gun.weapon.weaponId == SelectedCarInfo.Instance.WeaponID)
                gun.gameObject.SetActive(true);
            else
                gun.gameObject.SetActive(false);
        }
        
        _turretSpawner = FindObjectsOfType<TurretSpawn>();
        _pickupSpawners = FindObjectsOfType<PickupSpawner>();

        foreach(var turretSpawn in _turretSpawner)
            turretSpawn.gameObject.SetActive(false);

        foreach(var pickupSpawn in _pickupSpawners)
            pickupSpawn.gameObject.SetActive(false);

        while (_enabledTurretSpawn < 10 && _enabledPickupSpawn < 10)
        {
                foreach (var turretSpawn in _turretSpawner)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        turretSpawn.gameObject.SetActive(true);
                        _enabledTurretSpawn += 1;
                    }
                }

                foreach (var pickupSpawn in _pickupSpawners)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        pickupSpawn.gameObject.SetActive(true);
                        _enabledPickupSpawn += 1;
                    }
                }
            }
        }

    private void Start()
    {
        enemies = FindObjectsOfType<AI>();
        player = FindObjectOfType<CarController>();

        foreach(var enemy in enemies)
            enemiesList.Add(enemy);
    }

    private void Update()
    {
        UIManager.Instance.Timer();

        CheckResult();

        if (hasPlayerWon == true)
            UIManager.Instance.EnableWinPanel();

        else if (hasPlayerWon == false)
        {
            player.gameObject.SetActive(false);
            UIManager.Instance.EnableLosePanel();
        }

        for(int i = 0; i < enemiesList.Count; i++)
        {
            if(enemiesList[i].enemy.IsDead())
            {
                enemiesList[i].gameObject.SetActive(false);
                enemiesList.RemoveAt(i);
            }
        }
    }

    private void CheckResult()
    {
        if (player.player.IsDead() || UIManager.Instance.ReturnRemainingTime() < 0)
        {
            player.gameObject.SetActive(false);
            hasPlayerWon = false;
        }

        else if (enemies.Length <= 0 || (_enemyDeathCount > (enemies.Length / 2) && UIManager.Instance.ReturnRemainingTime() < 0))
        {
            hasPlayerWon = true;
        }
    }
}
