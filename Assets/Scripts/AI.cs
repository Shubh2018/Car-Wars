using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Enemy enemy;

    private float _nextTimeToFire = 0f;

    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _turretHead;

    [SerializeField] private Transform[] _projectileSpawn;
    [SerializeField] private GameObject projectile;

    private void Start()
    {
        _target = FindObjectOfType<CarController>().transform;
        enemy.SetHealth();
    }

    private void Update()
    {
        Vector3 directionToRotate = _target.position - _turretHead.position;
        Quaternion rotation = Quaternion.LookRotation(directionToRotate, Vector3.up);

        _turretHead.rotation = Quaternion.Lerp(_turretHead.rotation, rotation, 0.5f);

        if(Vector3.Distance(transform.position, _target.position) < enemy.range)
        {
            if (_nextTimeToFire < Time.time)
            {
                foreach (Transform spawn in _projectileSpawn)
                {
                    Instantiate(projectile.gameObject, spawn.position, _turretHead.rotation).transform.parent = this.transform;
                }

                _nextTimeToFire = Time.time + _fireRate;
            }
        }
    }
}
