using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _turrets;

    private void Start()
    {
        if(Random.Range(0, 100) < 50)
            Instantiate(_turrets[0], this.transform.position, Quaternion.identity).transform.parent = this.transform;
        else
            Instantiate(_turrets[1], this.transform.position, Quaternion.identity).transform.parent = this.transform;
    }
}
