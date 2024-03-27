using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;

    public PlayerHealth PHRef;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(this.gameObject);
            PHRef.StopHurtinMe();
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage; 


    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyHealth -= 10;
        }

    }

}
