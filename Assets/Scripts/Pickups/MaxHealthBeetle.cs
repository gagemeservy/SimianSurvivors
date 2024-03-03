using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthBeetle : MonoBehaviour, ICollectible
{
    public float healthToIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseMaxHealth(healthToIncrease);
        Destroy(gameObject);
    }
}
