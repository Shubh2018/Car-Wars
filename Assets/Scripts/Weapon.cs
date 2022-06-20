using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int weaponId;

    public int maxAmmo;
    public float fireRate;
    public int damagePerBullet;
    public int noOfBullets;

    public float critRate;

    private int currentAmmo;

    public Weapon()
    {
        weaponId = 0;
        maxAmmo = 0;
        fireRate = 0;
        damagePerBullet = 0;
        noOfBullets = 0;

        critRate = 0;
    }

    public bool CriticalDamage(float percent)
    {
        if (Random.Range(0f, 100f) < percent)
            return true;
        else
            return false;
    }

    public int TotalDamage()
    {
        return damagePerBullet * noOfBullets;
    }

    public void SetAmmo()
    {
        currentAmmo = maxAmmo;
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;

        if(currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }

    public void ReduceAmmo()
    {
        currentAmmo -= 1;

        if(currentAmmo < 0)
            currentAmmo = 0;
    }

    public int CurrentAmmo()
    {
        return currentAmmo;
    }
}
