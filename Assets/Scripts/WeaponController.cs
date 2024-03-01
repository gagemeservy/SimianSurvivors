using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float knockback;
    public float speed;
    public float freezeDuration;
    public int pierce;
    public int numberOfAttacksToDo;
    public float cooldownDuration;
    public float currentCooldown;
    
    protected PlayerController playerMovement;

    virtual protected void Start()
    {
        playerMovement = FindObjectOfType<PlayerController>();
        currentCooldown = cooldownDuration; //this instantly restarts the cooldown
    }


    virtual protected void Update()
    {
        currentCooldown -= Time.deltaTime;

        if(currentCooldown < 0)
        {
            Attack();
            currentCooldown = cooldownDuration;
        }
    }

    virtual protected void Attack()
    {
        
    }
}
