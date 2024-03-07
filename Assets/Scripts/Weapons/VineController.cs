using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class VineController : WeaponController
{
    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;
    int currentNumberOfAttacksToDo = 7;

    Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStats>().transform;
        currentNumberOfAttacksToDo = weaponData.NumberOfAttacksToDo;
        relativeSpawnPoints = weaponData.RelativeSpawnPoints;
    }

    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < currentNumberOfAttacksToDo; i++)
        {
            SpawnAndAttack(i);
        }
    }

    //protected override void lowerCoolDown(float amountToLowerBy)
    //{
    //    base.lowerCoolDown(amountToLowerBy);
    //}

    GameObject SpawnAndAttack(int i)
    {
        GameObject spawnedVine;
        Vector3 directionOffset;

        if (i < relativeSpawnPoints.Count)
        {
            directionOffset = relativeSpawnPoints[i].position;
            spawnedVine = Instantiate(weaponData.Prefab, player.position + relativeSpawnPoints[i].position, Quaternion.identity);
        }
        else
        {
            directionOffset = relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;
            spawnedVine = Instantiate(weaponData.Prefab, player.position + directionOffset, Quaternion.identity);
        }

        spawnedVine.GetComponent<VineBehavior>().DirectionSetter(directionOffset);

        //spawnedVine.GetComponent<SkeletonBehavior>().DirectionSetter(new Vector3(0, -1, 0));


        //DO THIS ON EVERY WEAPON
        spawnedVine.GetComponent<VineBehavior>().currentDamage = spawnedVine.GetComponent<VineBehavior>().currentDamage * damageMultiplier;


        return spawnedVine;
    }
}
