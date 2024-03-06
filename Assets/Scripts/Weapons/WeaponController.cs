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

    virtual protected void Start()
    {
        playerMovement = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();

        maxCooldown = weaponData.CooldownDuration;

        coolDownReductionValue = playerStats.currentAttackSpeed;
        damageMultiplier = playerStats.currentAttackDamage;

        lowerCoolDown(coolDownReductionValue);
        //currentCooldown = weaponData.cooldownDuration; //this instantly restarts the cooldown
        currentCooldown = -1;
    }


    virtual protected void Update()
    {
        if(playerStats.currentAttackSpeed != coolDownReductionValue)
        {
            maxCooldown += coolDownReductionValue;

            coolDownReductionValue = playerStats.currentAttackSpeed;
            lowerCoolDown(coolDownReductionValue);
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

    public float GetDamageAfterMultiplier(float baseDamage)
    {
        return baseDamage * damageMultiplier;
    }

    public void CoolDown(float amountToLowerBy)
    {
        //Debug.Log("in COOL DOWN");
        lowerCoolDown(amountToLowerBy);
    }

    virtual protected void lowerCoolDown(float amountToLowerBy)
    {
        //Debug.Log("in LOWER COOL DOWN");
        if (maxCooldown > (0 + amountToLowerBy))
        {
            maxCooldown -= amountToLowerBy;
        }
    }

    virtual protected void Attack()
    {
        
    }
}
