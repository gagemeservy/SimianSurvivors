using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpeedBeetle : Pickup, ICollectible
{
    public float speedToIncrease;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.HealthSpeedIncrease(speedToIncrease);

        if(GameObject.FindGameObjectWithTag("Audio") != null)
        {
            AudioManager audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            audioPlayer.PlaySFX(audioPlayer.LevelUpGetItem);
        }
    }
}
