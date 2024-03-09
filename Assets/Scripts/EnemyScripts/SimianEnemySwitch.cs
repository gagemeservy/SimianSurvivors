using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimianEnemySwitch : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    private SpriteRenderer sprite;

    public Sprite spriteToSwitch;

    public EnemyStats stats;

    public PlayerStats playerStats;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerStats = FindObjectOfType<PlayerStats>();
        Debug.Log("Simian Script Title is " + enemyData.ToString());
    }

    private void Update()
    {
        if (stats.currentHealth <= (enemyData.MaxHealth/2))
        {
            sprite.sprite = spriteToSwitch;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("DIED");
        playerStats.Win();
    }
}
