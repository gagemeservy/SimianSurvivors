using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    public float currentAttackDamage = 1;
    public float currentAttackSpeed = 0;
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
    public float experienceCapReductionPerLevel;


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


    /*[Header("InventoryWeaponsForPowerups")]
    public WeaponController weaponController1;
    public WeaponController weaponController2;
    public WeaponController weaponController3;
    public WeaponController weaponController4;*/

    InventoryManager inventory;
    public int weaponIndex;

    public List<GameObject> possibleWeapons = new List<GameObject>(4);

    public GameObject gameOverScreen;
    public GameObject levelDownScreen;
    public GameObject gameWinScreen;
    public Button levelDown1ActualButton;
    public Button levelDown2ActualButton;
    public Button levelDown3ActualButton;
    public TMP_Text levelDown1Button;
    public TMP_Text levelDown2Button;
    public TMP_Text levelDown3Button;
    public Image levelDown1Image;
    public Image levelDown2Image;
    public Image levelDown3Image;

    public int LevelDownsLeft = 0;
    AudioManager audioPlayer;
    [Header("LevelDownTimer")]
    float levelDownDuration = .1f;
    float levelDownTimer;

    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Audio") != null)
        {
            audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        
        
        inventory = GetComponent<InventoryManager>();
        //weaponIndex = 0;
        //weaponController = GetComponent<WeaponController>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        announcementBubble.SetActive(false);
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        //currentAttackDamage = characterData.AttackDamage;
        //currentAttackSpeed = characterData.AttackSpeed;
        currentAttackDamage = 1;
        currentAttackSpeed = 0;
        currentMaxHealth = characterData.MaxHealth;
        currentMagnet = characterData.Magnet;

        gameOverScreen.SetActive(false);
        levelDownScreen.SetActive(false);
        gameWinScreen.SetActive(false);



        //This function can be replicated with other numbers, of course it could be dynamic and I could pass in a number but I don't wanna waste time.
        Spawn3RandomWeapons();
    }

    private void Spawn3RandomWeapons()
    {
        if(possibleWeapons.Count > 0)
        {//1
            int randomSlot = UnityEngine.Random.Range(0, possibleWeapons.Count);

            SpawnWeapon(possibleWeapons[randomSlot]);

            possibleWeapons.RemoveAt(randomSlot);

            if (possibleWeapons.Count > 0)
            {//2
                randomSlot = UnityEngine.Random.Range(0, possibleWeapons.Count);

                SpawnWeapon(possibleWeapons[randomSlot]);

                possibleWeapons.RemoveAt(randomSlot);

                if (possibleWeapons.Count > 0)
                {//3
                    //Debug.Log("spawning third weapon");
                    randomSlot = UnityEngine.Random.Range(0, possibleWeapons.Count);

                    SpawnWeapon(possibleWeapons[randomSlot]);

                    possibleWeapons.RemoveAt(randomSlot);
                }
            }
        }
    }

    private void Update()
    {
        if (!PlayerController.isPaused)
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

            if (LevelDownsLeft > 0)
            {
                if(levelDownTimer <= 0)
                {
                    LevelDownsLeft--;
                    LevelDown();
                }
                else
                {
                    levelDownTimer -= Time.deltaTime;
                }
            }
        }
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
            experienceCapIncrease -= experienceCapReductionPerLevel;
            //LevelDown();
            //MAYBE CALL THIS FUNCTION AFTER AND PASS IT THE NUMBER OF LEVEL UPS OR SOMETHING
            LevelDownsLeft++;
        }
    }

    void LevelDown()
    {
        //Debug.Log("In level up! Weapon 1 level is " + inventory.weaponSlots[0].weaponData.Level);
        if(audioPlayer != null)
        {
            audioPlayer.PlaySFX(audioPlayer.LevelDown);
        }
       

        levelDownTimer = levelDownDuration;

        //BEFORE SETTING SCREEN ACTIVE. CHANGE ON THE LABELS ON THE BUTTONS.
        if (inventory.weaponSlots[0].weaponData.Level != 1)
        {
            levelDown1Button.SetText(inventory.weaponSlots[0].weaponData.Title + " Lv. " + (inventory.weaponSlots[0].weaponData.Level - 1) + " :\n" + inventory.weaponSlots[0].weaponData.Description);
            levelDown1Image.sprite = inventory.weaponSlots[0].weaponData.Icon;
        }
        else
        {
            levelDown1Button.SetText(inventory.weaponSlots[0].weaponData.Description);
            levelDown1ActualButton.enabled = false;
            //levelDown1Button.SetText(inventory.weaponSlots[0].weaponData.Description.ToString());
            //levelDown1Image.sprite = inventory.weaponSlots[0].weaponData.Icon;
        }

        if (inventory.weaponSlots[1].weaponData.Level != 1)
        {
            levelDown2Button.SetText(inventory.weaponSlots[1].weaponData.Title + " Lv. " + (inventory.weaponSlots[1].weaponData.Level - 1) + " :\n" + inventory.weaponSlots[1].weaponData.Description);
            levelDown2Image.sprite = inventory.weaponSlots[1].weaponData.Icon;
        }
        else
        {
            levelDown2Button.SetText(inventory.weaponSlots[1].weaponData.Description);
            levelDown2ActualButton.enabled = false;

        }

        if (inventory.weaponSlots[2].weaponData.Level != 1)
        {
            levelDown3Button.SetText(inventory.weaponSlots[2].weaponData.Title + " Lv. " + (inventory.weaponSlots[2].weaponData.Level - 1) + " :\n" + inventory.weaponSlots[2].weaponData.Description);
            levelDown3Image.sprite = inventory.weaponSlots[2].weaponData.Icon;
        }
        else
        {
            levelDown3Button.SetText(inventory.weaponSlots[2].weaponData.Description);
            levelDown3ActualButton.enabled = false;

        }


        
        if (inventory.weaponSlots[2].weaponData.Level == 1 && inventory.weaponSlots[1].weaponData.Level == 1 & inventory.weaponSlots[0].weaponData.Level == 1)
        {
            return;
        }
        else
        {
            levelDownScreen.SetActive(true);
            Time.timeScale = 0;
            PlayerController.isPaused = true;
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

            if (currentHealth < 1)
            {
                Kill();
            }
        }

    }

    public void Kill()
    {
        //Destroy(gameObject);
        //Bring up end game screen
        //Announce("YOU DIED");
        sprite.color = Color.black;

        Time.timeScale = 0;
        PlayerController.isPaused = true;
        gameOverScreen.SetActive(true);
        //PlayerController.Pause(gameOverScreen);

        //Destroy(gameObject);
        //END THE GAME
    }

    public void Win()
    {
        sprite.color = Color.white;

        Time.timeScale = 0;
        PlayerController.isPaused = true;
        gameWinScreen.SetActive(true);
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
        //Debug.Log("Increasing attack by " + attackIncrease);
        currentAttackDamage += attackIncrease;
        //Debug.Log("Attack is now " + currentAttackDamage);
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

    public void Announce(string text)
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

        if(weaponIndex >= inventory.weaponSlots.Count)
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
