using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBeetle : Pickup, ICollectible
{
    public float healthToRestore;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }
}
