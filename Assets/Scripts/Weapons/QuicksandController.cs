using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuicksandController : WeaponController
{
    int currentNumberOfAttacksToDo = 4;
    int spawnRadius1 = 10;
    int spawnRadius2 = 5;

    int[,] locationArray = new int[4, 2];

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        locationArray[0,0] = spawnRadius1;
        locationArray[0, 1] = spawnRadius2;
        locationArray[1, 0] = -spawnRadius1;
        locationArray[1, 1] = -spawnRadius2;
        locationArray[2, 0] = -spawnRadius1;
        locationArray[2, 1] = spawnRadius2;
        locationArray[3, 0] = spawnRadius1;
        locationArray[3, 1] = -spawnRadius2;
        currentNumberOfAttacksToDo = weaponData.NumberOfAttacksToDo;
    }

    protected override void Attack()
    {
        base.Attack();
        audioPlayer.PlaySFX(audioPlayer.quicksand);
        for (int i = 0; i < currentNumberOfAttacksToDo; i++)
        {
            SpawnAndAttack(i);
        }
    }

    GameObject SpawnAndAttack(int i)
    {
        
        GameObject spawnedQuicksand = Instantiate(weaponData.Prefab);
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += locationArray[i, 0];
        spawnPosition.y += locationArray[i, 1];

        spawnedQuicksand.transform.position = spawnPosition; //this sets it based on the player's position

        spawnedQuicksand.GetComponent<QuicksandBehavior>().currentDamage = GetDamageAfterMultiplier(spawnedQuicksand.GetComponent<QuicksandBehavior>().currentDamage);

        return spawnedQuicksand;
    }
}
