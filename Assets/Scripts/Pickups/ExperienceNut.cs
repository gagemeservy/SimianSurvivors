using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceNut : MonoBehaviour, ICollectible
{
    public int xpGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(xpGranted);
        Destroy(gameObject);
    }
}
