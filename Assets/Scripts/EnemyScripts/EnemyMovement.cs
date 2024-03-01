using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float movespeed;
    private bool flip;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        flip = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);
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
