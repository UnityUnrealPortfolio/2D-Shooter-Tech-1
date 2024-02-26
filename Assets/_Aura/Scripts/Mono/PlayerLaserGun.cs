using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaserGun : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] Transform laserFirePoint;
    [SerializeField] float weaponCoolDown;
    [SerializeField] int laserCount;

    float timeTilNextFire;
    bool canFire;
    private List<GameObject> laserPool = new List<GameObject>();

    private void Awake()
    {
        
        InitializeLaserPool();
    }

    void ShootLaser()
    {
        if(canFire && timeTilNextFire < Mathf.Epsilon)
        {
          timeTilNextFire = weaponCoolDown;
          
        }

    }

    public void HandleFireInput(InputAction.CallbackContext context)
    {
        if ((context.performed))
        {
            canFire = true;
        }
    }

    private void InitializeLaserPool()
    {
        Debug.Log("Inside InitializePool()");
        //initialize pool
        GameObject go;
        for (int i = 0; i < laserPool.Count; i++)
        {
            print("Running loop");
            go = Instantiate(laser.gameObject, transform.position, Quaternion.identity);
            go.gameObject.SetActive(false);
            go.transform.parent = transform.parent;
            laserPool.Add(go);
        }
    }
}
