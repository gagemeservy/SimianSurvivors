using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Attack()
    {
        base.Attack();
        //NEED TO CHECK NUMBEROFATTACKSTODO and instantiate a new coconut for each one and spread out their directions a bit
        GameObject spawnedCoconut = Instantiate(weaponData.Prefab);
        spawnedCoconut.transform.position = transform.position; //this sets it to the player's position

        newDirection = spawnedCoconut.GetComponent<CoconutBehavior>().DirectionChecker(playerMovement.moveDirection, previousDirection);

        //this makes sure it goes in the last direction the player shot if they are standing still
        if (newDirection != Vector3.zero)
        {
            previousDirection = newDirection;
        }
    }
}
