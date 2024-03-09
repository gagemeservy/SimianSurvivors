using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float currentCooldown;
    private float maxCooldown;

    public float coolDownReductionValue;
    public float damageMultiplier = 1;

    protected PlayerController playerMovement;
    protected Vector3 previousDirection = Vector3.one;
    protected Vector3 newDirection;

    protected PlayerStats playerStats;

    protected AudioManager audioPlayer;

    virtual protected void Start()
    {
        playerMovement = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();

        if (GameObject.FindGameObjectWithTag("Audio") != null)
        {
            audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        maxCooldown = weaponData.CooldownDuration;

        coolDownReductionValue = playerStats.currentAttackSpeed;
        damageMultiplier = playerStats.currentAttackDamage;

        if (coolDownReductionValue > 0)
        {
            if ((maxCooldown - coolDownReductionValue) > 0.3)
            {
                maxCooldown -= coolDownReductionValue;
            }
            else
            {
                maxCooldown = 0.3f;
            }
        }

        //lowerCoolDown(coolDownReductionValue);
        //currentCooldown = weaponData.cooldownDuration; //this instantly restarts the cooldown
        currentCooldown = -1;


    }


    virtual protected void Update()
    {
        if (!PlayerController.isPaused)
        {
            if (playerStats.currentAttackSpeed != coolDownReductionValue)
            {
                float oldCoolDownReductionValue = coolDownReductionValue;
                maxCooldown += oldCoolDownReductionValue;

                coolDownReductionValue = playerStats.currentAttackSpeed;
                bool success = lowerCoolDown(coolDownReductionValue);

                if (!success)
                {
                    maxCooldown -= oldCoolDownReductionValue;
                    coolDownReductionValue = oldCoolDownReductionValue;
                }
            }

            damageMultiplier = playerStats.currentAttackDamage;

            currentCooldown -= Time.deltaTime;

            newDirection = playerMovement.moveDirection;

            if (newDirection != Vector3.zero)
            {
                previousDirection = newDirection;
            }

            if (currentCooldown < 0)
            {
                Attack();
                currentCooldown = maxCooldown;
            }
        }
    }

    public float GetDamageAfterMultiplier(float baseDamage)
    {
        return baseDamage * damageMultiplier;
    }

    public void CoolDown(float amountToLowerBy)
    {
        //Debug.Log("in COOL DOWN");
        lowerCoolDown(amountToLowerBy);
    }

    virtual protected bool lowerCoolDown(float amountToLowerBy)
    {
        //Debug.Log("in LOWER COOL DOWN");
        if (maxCooldown > (0.2 + amountToLowerBy))
        {
            maxCooldown -= amountToLowerBy;
            return true;
        }

        return false;
    }

    virtual protected void Attack()
    {
        
    }
}
