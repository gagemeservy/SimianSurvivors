using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthBeetle : Pickup, ICollectible
{
    public float healthToIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseMaxHealth(healthToIncrease);
    }
}
