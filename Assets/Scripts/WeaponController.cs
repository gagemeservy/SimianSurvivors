using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float currentCooldown;
    
    protected PlayerController playerMovement;

    virtual protected void Start()
    {
        playerMovement = FindObjectOfType<PlayerController>();
        //currentCooldown = weaponData.cooldownDuration; //this instantly restarts the cooldown
        currentCooldown = -1;
    }


    virtual protected void Update()
    {
        currentCooldown -= Time.deltaTime;

        if(currentCooldown < 0)
        {
            Attack();
            currentCooldown = weaponData.CooldownDuration;
        }
    }

    virtual protected void Attack()
    {
        
    }
}
