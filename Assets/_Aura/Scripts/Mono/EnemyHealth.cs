using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("ship health. when zero, ship is destroyed")][SerializeField] int hitPoints;
    [Tooltip("Total points earned for destroying ship")][SerializeField] int scoreValue;
    public int HitPoints
    {
        get
        {
            return hitPoints;
        }
        private set
        {
            hitPoints = value;
            if (hitPoints <= 0)
            {
                //UpgradePlayerScore();
                DestroyShip();
            }
        }
    }

    private void UpgradePlayerScore()
    {
      //ToDo:update score in player manager
    }

    private void DestroyShip()
    {
       gameObject.SetActive(false);
    }

    public void TakeDamage(int _damage)
    {
        HitPoints-=_damage;
        //Play damage animation
        //Play damage fx etc
    }
}
