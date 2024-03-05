using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class VineController : WeaponController
{
    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;
    int currentNumberOfAttacksToDo = 6;

    Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStats>().transform;
    }

    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < currentNumberOfAttacksToDo; i++)
        {
            SpawnAndAttack(i);
        }
    }

    protected override void lowerCoolDown(float amountToLowerBy)
    {
        base.lowerCoolDown(amountToLowerBy);
    }

    GameObject SpawnAndAttack(int i)
    {
        GameObject spawnedVine;

        if (i < relativeSpawnPoints.Count)
        {
            spawnedVine = Instantiate(weaponData.Prefab, player.position + relativeSpawnPoints[i].position, Quaternion.identity);
        }
        else
        {
            spawnedVine = Instantiate(weaponData.Prefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
        }

        //spawnedVine.GetComponent<SkeletonBehavior>().DirectionSetter(new Vector3(0, -1, 0));


        //DO THIS ON EVERY WEAPON
        spawnedVine.GetComponent<VineBehavior>().currentDamage = spawnedVine.GetComponent<VineBehavior>().currentDamage * damageMultiplier;


        return spawnedVine;
    }
}
