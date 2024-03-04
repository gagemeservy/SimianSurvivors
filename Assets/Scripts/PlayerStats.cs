using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentAttackDamage;
    [HideInInspector]
    public float currentAttackSpeed;
    [HideInInspector]
    public float currentMaxHealth;

    public GameObject announcementBubble;
    public TMP_Text playerAnnouncements;

    [Header("Experience/Level")]
    public float experience = 0;
    public int level = 1;
    public float experienceCap = 100;
    public float experienceCapIncrease;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("AnnounceDuration")]
    public float announceDuration;
    float announceTimer;

    private void Awake()
    {
        announcementBubble.SetActive(false);
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentAttackDamage = characterData.AttackDamage;
        currentAttackSpeed = characterData.AttackSpeed;
        currentMaxHealth = characterData.MaxHealth;

    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        if (announceTimer > 0)
        {
            announceTimer -= Time.deltaTime;
        }
        else
        {
            announcementBubble.SetActive(false);
        }
        
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    private void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            //CALL THE FUNCTION TO LET THEM SELECT AN ITEM TO LEVEL DOWN
            level++;
            experience -= experienceCap;
            experienceCap = experienceCap + (experienceCap * experienceCapIncrease);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }

    }

    public void Kill()
    {
        Destroy(gameObject);
        //END THE GAME
    }

    public void RestoreHealth(float healthToRestore)
    {
        Announce("Health Restored!");

        if (currentHealth < currentMaxHealth)
        {
            currentHealth += healthToRestore;

            if (currentHealth > currentMaxHealth)
            {
                currentHealth = currentMaxHealth;
            }
        }
    }

    void Recover()
    {
        if(currentHealth < currentMaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
        }
    }

    public void IncreaseMaxHealth(float healthToIncrease)
    {
        Announce("Max Health Increased!");
            
        currentMaxHealth += healthToIncrease;
    }

    public void HealthSpeedIncrease(float speedToIncrease)
    {
        Announce("Health Recovery Speed Increased!");

        currentRecovery += speedToIncrease;
    }

    public void IncreaseAttack(float attackIncrease)
    {
        Announce("Attack Power Increased!");

        currentAttackDamage += attackIncrease;
    }

    public void IncreaseAttackSpeed(float attackSpeedIncrease)
    {
        Announce("Attack Speed Increased!");

        currentAttackSpeed += attackSpeedIncrease;
    }

    public void IncreaseMoveSpeed(float speedIncrease)
    {
        Announce("Move Speed Increased!");

        currentMoveSpeed += speedIncrease;
    }

    void Announce(string text)
    {
        announceTimer = announceDuration;
        announcementBubble.SetActive(true);
        this.playerAnnouncements.SetText(text);
    }
}
