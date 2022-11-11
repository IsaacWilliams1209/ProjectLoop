using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEnemy : Entity
{
    public int weaponIndex;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Health;
        weapon = new Weapon(Weapons.weaponList[weaponIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameManager.player.transform);
        weapon.Update();
        weapon.Fire(transform.position, GameManager.player.transform.position);
    }

    
}
