using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionScript : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        if (player.GetHealthSlider() == null)
            return;
    }
}
