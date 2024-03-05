using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a base script that should be put on all projectile weapon prefabs
public class ProjectileWeaponBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;

    //current weapon stats
    protected float currentDamage;
    protected float currentKnockback;
    protected float currentSpeed;
    protected float currentFreezeDuration;
    protected float currentPierce;
    public float currentNumberOfAttacksToDo;
    protected float currentCooldownDuration;
    protected float currentForce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentKnockback = weaponData.Knockback; 
        currentSpeed = weaponData.Speed;
        currentFreezeDuration = weaponData.FreezeDuration;
        currentPierce = weaponData.Pierce;
        currentNumberOfAttacksToDo = weaponData.NumberOfAttacksToDo;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentForce = weaponData.CurrentForce;


}

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public Vector3 DirectionChecker(Vector3 givenDirection, Vector3 previousDirection)
    {
        if (givenDirection != Vector3.zero)
        {
            this.direction = givenDirection;
        }
        else
        {
            this.direction = previousDirection;
        }

        return this.direction;
    }

    public void DirectionSetter(Vector3 givenDirection)
    {
        this.direction = givenDirection;
    }

    protected virtual void OnTriggerStay2D(Collider2D collidedWith)
    {
        //call take damage on the enemy
        if (collidedWith.CompareTag("Enemy"))
        {
            EnemyStats enemy = collidedWith.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
    }

    private void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
