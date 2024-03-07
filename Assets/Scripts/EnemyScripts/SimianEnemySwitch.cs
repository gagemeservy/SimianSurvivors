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

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log("Simian Script Title is " + enemyData.ToString());
    }

    private void Update()
    {
        if (stats.currentHealth <= (enemyData.MaxHealth/2))
        {
            sprite.sprite = spriteToSwitch;
        }
    }
}
