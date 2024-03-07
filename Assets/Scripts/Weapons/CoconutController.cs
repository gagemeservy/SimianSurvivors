using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutController : WeaponController
{
    int currentNumberOfAttacksToDo = 8;

    float[,] locationArray = new float[8, 2];
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        locationArray[0, 0] = 0;
        locationArray[0, 1] = 1;

        locationArray[1, 0] = 1;
        locationArray[1, 1] = 0;

        locationArray[2, 0] = 0;
        locationArray[2, 1] = -1;

        locationArray[3, 0] = -1;
        locationArray[3, 1] = 0;

        locationArray[4, 0] = -.71f;
        locationArray[4, 1] = .71f;

        locationArray[5, 0] = .71f;
        locationArray[5, 1] = .71f;

        locationArray[6, 0] = .71f;
        locationArray[6, 1] = -.71f;

        locationArray[7, 0] = -.71f;
        locationArray[7, 1] = -.71f;
        currentNumberOfAttacksToDo = weaponData.NumberOfAttacksToDo;
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
    //    //Debug.Log("Coconut cooldown");
    //    base.lowerCoolDown(amountToLowerBy);
    //}

    GameObject SpawnAndAttack(int i)
    {
        GameObject spawnedCoconut = Instantiate(weaponData.Prefab);
        spawnedCoconut.transform.position = transform.position;
        spawnedCoconut.GetComponent<CoconutBehavior>().DirectionSetter(new Vector3(locationArray[i, 0], locationArray[i, 1], 0));


        //DO THIS ON EVERY WEAPON
        spawnedCoconut.GetComponent<CoconutBehavior>().currentDamage = GetDamageAfterMultiplier(spawnedCoconut.GetComponent<CoconutBehavior>().currentDamage);


        return spawnedCoconut;
    }
}
