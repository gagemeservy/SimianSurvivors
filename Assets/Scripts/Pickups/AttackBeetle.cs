using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBeetle : MonoBehaviour, ICollectible
{
    public float attackIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseAttack(attackIncrease);
        Destroy(gameObject);
    }
}
