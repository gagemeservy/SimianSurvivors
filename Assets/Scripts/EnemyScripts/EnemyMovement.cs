using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime);
        Vector3 oldScale = transform.localScale;

        //this flips the enemy sprite
        if ((transform.position.x - player.transform.position.x) > 0 && (oldScale.x > 0))
        {
            
            oldScale.Set(-oldScale.x, oldScale.y, oldScale.z);
            transform.localScale = oldScale;
        }
        else if ((transform.position.x - player.transform.position.x) < 0 && (oldScale.x < 0))
        {
            oldScale.Set(-oldScale.x, oldScale.y, oldScale.z);
            transform.localScale = oldScale;
        }
    }
}
