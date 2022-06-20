using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public bool healthPickUp;
    public bool ammoPickUp;

    public float health;
    public int ammo;

    /*private CarController player;
    private Gun weapon;*/

    private void Update()
    {
        transform.Rotate(Vector3.up, 30.0f * Time.deltaTime);
    }
}
