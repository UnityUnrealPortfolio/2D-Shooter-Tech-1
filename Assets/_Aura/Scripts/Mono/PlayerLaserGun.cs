using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaserGun : MonoBehaviour
{
    [SerializeField] Transform laser;
    [SerializeField] Transform laserFirePoint;
    [SerializeField] float weaponCoolDown;
    [SerializeField] int laserCount;

    float timeTilNextFire;
    bool canFire;
    private List<Transform> laserPool = new List<Transform>();

    private void Awake()
    {
        
        InitializeLaserPool();
    }

    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        if (canFire && timeTilNextFire < Mathf.Epsilon)
        {
            timeTilNextFire = weaponCoolDown;
            //get a laser from the pool
            GetLaser(laserFirePoint.position,laserFirePoint.rotation);
           canFire = false;
        }
        timeTilNextFire -= Time.deltaTime;

    }

    public void HandleFireInput(InputAction.CallbackContext context)
    {
        if ((context.performed))
        {
            print("Firing");
            canFire = true;
        }
    }

    private GameObject GetLaser(Vector3 _launchPos,Quaternion rot)
    {
        print("Inside GetLaser()");
        foreach (Transform t in laserPool)
        {
            if (t.gameObject.activeSelf == false)
            {
                print("Found one bosss!");
                t.gameObject.SetActive(true);
                t.position = _launchPos;
                t.rotation = rot;
                return t.gameObject;
            }
        }
           
        return null;
    }
    private void InitializeLaserPool()
    {
        Debug.Log("Inside InitializePool()");
        //initialize pool

        Transform go;
        for (int i = 0; i < laserCount; i++)
        {
            print("Running loop");
            go = Instantiate(laser, transform.position,laserFirePoint.rotation);
            go.gameObject.SetActive(false);
            go.parent = transform.parent;
            laserPool.Add(go);
        }


    }
}
