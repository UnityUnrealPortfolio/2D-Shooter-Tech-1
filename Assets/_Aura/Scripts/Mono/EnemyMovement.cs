using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float minEnemySpeed;
    [SerializeField] float maxEnemySpeed;
    [Tooltip("How many laser hits to destroy")]
   
    [SerializeField] float stopDistance;

    float speedAtSpawn;
    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speedAtSpawn = Random.Range(minEnemySpeed, maxEnemySpeed);
    }

    private void Update()
    {
        if (target != null)
        {
            
            float distance = Vector2.Distance(target.position, transform.position);
            if (distance > stopDistance)
            {
                transform.position =  Vector2.MoveTowards(transform.position,target.position,speedAtSpawn * Time.deltaTime);
            }
        }
    }

}
