using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Health;

    protected int currentHealth;

    public int armour;

    public virtual void TakeDamage(int damage)
    {
        armour -= damage;
        if (armour < 0)
        {
            currentHealth -= armour;
            armour = 0;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // Die
            // Drop Loot
        }
    }
}
