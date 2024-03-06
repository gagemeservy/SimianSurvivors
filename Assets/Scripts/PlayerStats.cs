using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

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
    [HideInInspector]
    public float currentMagnet;

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

    [Header("FlashDuration")]
    float flashDuration = .1f;
    float flashTimer = 0;
    private Color colorToFlash = Color.red;
    private Color originalColor;
    private SpriteRenderer sprite;


    [Header("InventoryWeaponsForPowerups")]
    public WeaponController weaponController1;
    public WeaponController weaponController2;
    public WeaponController weaponController3;
    public WeaponController weaponController4;

    InventoryManager inventory;
    public int weaponIndex;

    public List<GameObject> possibleWeapons = new List<GameObject>(4);
    public GameObject secondWeaponTest;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        //weaponIndex = 0;
        //weaponController = GetComponent<WeaponController>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        announcementBubble.SetActive(false);
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentAttackDamage = characterData.AttackDamage;
        currentAttackSpeed = characterData.AttackSpeed;
        currentMaxHealth = characterData.MaxHealth;
        currentMagnet = characterData.Magnet;

        //If anything, I can spawn them with random weapons

        SpawnWeapon(possibleWeapons[0]);
        SpawnWeapon(secondWeaponTest);
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

        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
        }
        else
        {
            sprite.color = originalColor;
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
        while (experience >= experienceCap)
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
            FlashDamage();

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
        Announce("Attack Multiplier Increased!");

        currentAttackDamage += attackIncrease;
        weaponController1.damageMultiplier = currentAttackDamage;
        weaponController2.damageMultiplier = currentAttackDamage;
        weaponController3.damageMultiplier = currentAttackDamage;
        weaponController4.damageMultiplier = currentAttackDamage;
    }

    public void IncreaseAttackSpeed(float attackSpeedIncrease)
    {
        Announce("Attack Speed Increased!");
        currentAttackSpeed += attackSpeedIncrease;
        weaponController1.CoolDown(attackSpeedIncrease);
        weaponController2.CoolDown(attackSpeedIncrease);
        weaponController3.CoolDown(attackSpeedIncrease);
        weaponController4.CoolDown(attackSpeedIncrease);
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

    private void FlashDamage()
    {
        flashTimer = flashDuration;
        sprite.color = colorToFlash;
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //Debug.Log("In weapon spawner");

        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots are full");
            return;
        }

        //Debug.Log("Spawning weapon");
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        //Debug.Log("Spawned " + spawnedWeapon.name);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }
}
