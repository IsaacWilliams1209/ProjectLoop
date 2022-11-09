using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEnemy : MonoBehaviour
{
    public int maxHealth;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
           // Die
           // Drop Loot
        }
    }
}
