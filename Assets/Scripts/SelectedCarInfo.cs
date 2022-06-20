using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCarInfo : MonoBehaviour
{
    private static SelectedCarInfo instance;

    public static SelectedCarInfo Instance
    {
        get { return instance; }
    }

    private int carID;

    public int CarID
    {
        get { return carID; }
        set { carID = value; }
    }

    private int weaponID;

    public int WeaponID
    {
        get { return weaponID; }
        set { weaponID = value; }
    }

    private bool isTimeLimitGame = false;

    public bool IsTimeLimitGame
    {
        get { return isTimeLimitGame; }
        set { isTimeLimitGame = value; }
    }

    private float timeLimit;

    public float TimeLimit
    {
        get { return timeLimit; }
        set { timeLimit = value; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;

        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
}
