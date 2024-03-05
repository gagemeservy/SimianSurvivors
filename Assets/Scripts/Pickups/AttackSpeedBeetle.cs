using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBeetle : Pickup, ICollectible
{
    public float attackSpeedIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseAttackSpeed(attackSpeedIncrease);
    }
}
