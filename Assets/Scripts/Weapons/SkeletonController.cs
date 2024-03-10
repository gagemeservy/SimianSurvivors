using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class SkeletonController : WeaponController
{
    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;
    int currentNumberOfAttacksToDo = 9;

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
        if (GameObject.FindGameObjectWithTag("Audio") != null)
        {
            audioPlayer.PlaySFX(audioPlayer.skeleton);
        }
            
        for (int i = 0; i < currentNumberOfAttacksToDo; i++)
        {
            SpawnAndAttack(i);
        }
    }

    //protected override bool lowerCoolDown(float amountToLowerBy)
    //{
    //    base.lowerCoolDown(amountToLowerBy);
    //}

    GameObject SpawnAndAttack(int i)
    {
        GameObject spawnedSkeleton;

        if (i < relativeSpawnPoints.Count){
            spawnedSkeleton = Instantiate(weaponData.Prefab, player.position + relativeSpawnPoints[i].position, Quaternion.identity);
        }
        else
        {
            spawnedSkeleton = Instantiate(weaponData.Prefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
        }

        spawnedSkeleton.GetComponent<SkeletonBehavior>().DirectionSetter(new Vector3(0, -1, 0));


        //DO THIS ON EVERY WEAPON
        spawnedSkeleton.GetComponent<SkeletonBehavior>().currentDamage = spawnedSkeleton.GetComponent<SkeletonBehavior>().currentDamage * damageMultiplier;


        return spawnedSkeleton;
    }
}
