using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBeetle : Pickup, ICollectible
{
    public float attackIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseAttack(attackIncrease);
        AudioManager audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioPlayer.PlaySFX(audioPlayer.LevelUpGetItem);
    }
}
