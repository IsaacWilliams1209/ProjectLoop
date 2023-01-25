using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoreEnemy : Entity
{
    public int weaponIndex;

    NavMeshAgent nav;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        currentHealth = Health;
        weapon = new Weapon(Weapons.weaponList[weaponIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameManager.player.transform);
        nav.SetDestination(GameManager.player.transform.position);
        weapon.Update();
        weapon.Fire(transform.position, GameManager.player.transform.position);
    }    
}
