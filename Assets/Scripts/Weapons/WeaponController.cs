using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float currentCooldown;
    private float maxCooldown;
    public float damageMultiplier = 1;

    protected PlayerController playerMovement;
    protected Vector3 previousDirection = Vector3.one;
    protected Vector3 newDirection;

    virtual protected void Start()
    {
        playerMovement = FindObjectOfType<PlayerController>();
        maxCooldown = weaponData.CooldownDuration;
        //currentCooldown = weaponData.cooldownDuration; //this instantly restarts the cooldown
        currentCooldown = -1;
    }


    virtual protected void Update()
    {
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
