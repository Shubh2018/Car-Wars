using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectionUI : MonoBehaviour
{
    [SerializeField] private CarSelectionScript[] cars;

    [SerializeField] private Slider _maxSpeed;
    [SerializeField] private Slider _maxHealth;
    [SerializeField] private Slider _control;

    private WeaponSelectionUI[] _weapons;

    [SerializeField] private Slider _maxAmmo;
    [SerializeField] private Slider _fireRate;
    [SerializeField] private Slider _damage;
    [SerializeField] private Slider _critRate;

    private int _carIndex = 0;
    private int _weaponIndex = 0;

    private List<WeaponSelectionUI[]> _weaponsList = new List<WeaponSelectionUI[]>();

    [SerializeField] private CanvasGroup _carSelectionCanvas;
    [SerializeField] private CanvasGroup _weaponSelectionCanvas;
    [SerializeField] private CanvasGroup _timeSelectionCanvas;

    private void Awake()
    {
        foreach (var car in cars)
        {
            _weapons = car.GetComponentsInChildren<WeaponSelectionUI>();

            _weaponsList.Add(_weapons);

            for(int i = 0; i < _weapons.Length; i++)
            {
                _weapons[i].gameObject.SetActive(false);
            }

            car.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        EnableCarSelectionUI();
        DisableWeaponSelectionUI();
        DisableTimeSelectUI();
    }

    private void Update()
    {
        EnableCarAtIndex(_carIndex);

        if(_weapons != null)
            EnableWeaponOnIndex(_weaponIndex);
    }

    private void EnableCarAtIndex(int index)
    {
        foreach(var car in cars)
        {
            if(car.player.carID == index)
            {
                car.gameObject.SetActive(true);
                _maxSpeed.value = car.player.speedMultiplier;
                _maxHealth.value = (float)(car.player.maxHealth / 150);
                _control.value = car.player.control;
            }

            else
            {
                car.gameObject.SetActive(false);
            }
        }
    }

    private void EnableWeaponOnIndex(int index)
    {
        foreach(var weapon in _weapons)
        {
            if(weapon.weapon.weaponId == _weaponIndex)
            {
                weapon.gameObject.SetActive(true);
                _maxAmmo.value = (float) (weapon.weapon.maxAmmo/100f);
                _fireRate.value = (float)(weapon.weapon.fireRate / 2.5f);
                _damage.value = (float)(weapon.weapon.TotalDamage() / 24f);
                _critRate.value = (float)(weapon.weapon.critRate / 100f);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void ConfirmCar()
    {
        _weapons = _weaponsList[_carIndex];

        DisableCarSelectionUI();
        EnableWeaponSelectionUI();
    }

    public void IncreaseCarIndex()
    {
        _carIndex += 1;

        if (_carIndex > 3)
            _carIndex = 0;
    }

    public void DecreaseCarIndex()
    {
        _carIndex -= 1;

        if (_carIndex < 0)
            _carIndex = 3;
    }

    public void IncreaseWeaponIndex()
    {
        _weaponIndex += 1;

        if (_weaponIndex > 2)
            _weaponIndex = 0;
    }

    public void DecreaseWeaponIndex()
    {
        _weaponIndex -= 1;

        if (_weaponIndex < 0)
            _weaponIndex = 0;
    }

    public void EnableCarSelectionUI()
    {
        _carSelectionCanvas.alpha = 1.0f;
        _carSelectionCanvas.interactable = true;
        _carSelectionCanvas.blocksRaycasts = true;
    }

    public void DisableCarSelectionUI()
    {
        _carSelectionCanvas.alpha = 0.0f;
        _carSelectionCanvas.interactable = false;
        _carSelectionCanvas.blocksRaycasts = false;
    }

    public void EnableWeaponSelectionUI()
    {
        _weaponSelectionCanvas.alpha = 1.0f;
        _weaponSelectionCanvas.interactable = true;
        _weaponSelectionCanvas.blocksRaycasts = true;
    }

    public void DisableWeaponSelectionUI()
    {
        _weaponSelectionCanvas.alpha = 0.0f;
        _weaponSelectionCanvas.interactable = false;
        _weaponSelectionCanvas.blocksRaycasts = false;
    }

    public void StartGame()
    {
        SelectedCarInfo.Instance.CarID = _carIndex;
        SelectedCarInfo.Instance.WeaponID = _weaponIndex;

        SceneManager.LoadScene("GameScene");
    }

    public void BackToCarSelection()
    {
        DisableWeaponSelectionUI();
        EnableCarSelectionUI();
    }

    public void MainMenu()
    { 
        SceneManager.LoadScene("MainMenu");
    }

    public void EnableTimeSelectUI()
    {
        _timeSelectionCanvas.alpha = 1.0f;
        _timeSelectionCanvas.interactable = true;
        _timeSelectionCanvas.blocksRaycasts= true;
    }

    public void DisableTimeSelectUI()
    {
        _timeSelectionCanvas.alpha = 0.0f;
        _timeSelectionCanvas.interactable = false;
        _timeSelectionCanvas.blocksRaycasts = false;
    }

    public void BackToWeaponSelect()
    {
        DisableTimeSelectUI();
        EnableWeaponSelectionUI();
    }

    public void TimeLimt(float time)
    {
        if (time == 0.0f)
        {
            //SelectedCarInfo.Instance.TimeLimit = time;
            SelectedCarInfo.Instance.IsTimeLimitGame = false;
        }

        else
        {
            SelectedCarInfo.Instance.IsTimeLimitGame = true;
            SelectedCarInfo.Instance.TimeLimit = time;
        }
    }
}
