using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public EnemyScriptableObject enemyData;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
        Vector3 oldScale = transform.localScale;

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
