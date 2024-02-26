using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipPool : MonoBehaviour
{
    [Tooltip("The ship prefab to pool")]
    [SerializeField]Transform enemyShip;
    [Tooltip("Number of ship to pool")] 
    [SerializeField] int shipCount;

    List<Transform> shipPool = new List<Transform>();

    private void Awake()
    {
        InitializeShipPool();
    }

    public GameObject GetPoolObject()
    {
        foreach (var item in shipPool)
        {
            if(item.gameObject.activeSelf == false)
            {
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }
        return null;    
    }
    private void InitializeShipPool()
    {
       Transform t;
       for (int i = 0; i < shipCount; i++)
        {
           t = Instantiate(enemyShip,transform.position,Quaternion.identity); 
           t.parent = transform;
           t.gameObject.SetActive(false);
           shipPool.Add(t);
        }
    }
}
