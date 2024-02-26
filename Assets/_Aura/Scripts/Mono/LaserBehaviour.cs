using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] float laserSpeed;
    [SerializeField] int laserDamage;
    [SerializeField] Rigidbody2D laserRb;
    [SerializeField] float laserLifeTime;

    private void OnEnable()
    {
        StartCoroutine(LaserDestruction());
    }
    void Update()
    {
        transform.Translate(Vector2.up * laserSpeed * Time.deltaTime);
    }

    IEnumerator LaserDestruction()
    {
        yield return new WaitForSeconds(laserLifeTime);
        gameObject.SetActive(false);//ToDo:consider Resetting it before inactivating
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(laserDamage);
            }
            gameObject.SetActive(false);
        }
    }
}
