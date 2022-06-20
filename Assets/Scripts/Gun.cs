using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Weapon weapon;

    private float _nextTimeToFire = 0;

    private Button _fireButton;

    private bool _canFire = true;

    [SerializeField] private Transform[] _projectileSpawn;
    [SerializeField] private GameObject _projectile;

    private void Awake()
    {
        _fireButton = GameObject.Find("Fire_Button").GetComponent<Button>();
        _fireButton.onClick.AddListener(() => Fire());
    }

    private void Start()
    {
        weapon.SetAmmo();
    }

    private void Update()
    {
        if (weapon.CurrentAmmo() <= 0)
            _canFire = false;

        else
            _canFire = true;
    }

    void Fire()
    {
        if (_nextTimeToFire < Time.time && _canFire)
        {
            foreach (Transform spawn in _projectileSpawn)
            {
                Instantiate(_projectile, spawn.transform.position, spawn.rotation).transform.parent = this.transform;
                weapon.ReduceAmmo();
            }

            _nextTimeToFire = Time.time + weapon.fireRate;
        }
    }
}
