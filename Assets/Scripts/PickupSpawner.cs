using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _pickups;

    private void Awake()
    {
        if (Random.Range(0, 100) < 25)
            Instantiate(_pickups[0], this.transform.position, Quaternion.identity).transform.parent = this.transform;
        else if (Random.Range(0, 100) < 50)
            Instantiate(_pickups[1], this.transform.position, Quaternion.identity).transform.parent = this.transform;
        else if (Random.Range(0, 100) < 75)
            Instantiate(_pickups[2], this.transform.position, Quaternion.identity).transform.parent = this.transform;
        else
            Instantiate(_pickups[3], this.transform.position, Quaternion.identity).transform.parent = this.transform;
    }
}
