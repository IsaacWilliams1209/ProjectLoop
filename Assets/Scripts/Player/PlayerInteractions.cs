using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    GameObject closestPickup;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.gameObject.layer == 12)
        {
            closestPickup = other.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && closestPickup != null)
        {
            Debug.Log("Picking up Weapon");
            PickupWeapon(closestPickup);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        closestPickup = null;
    }

    public void PickupWeapon(GameObject pickup)
    {
        Weapon weapon = new Weapon(pickup.GetComponent<WeaponPickup>().weapon);
        Weapon temp = new Weapon(GameManager.player.equippedWeapon);
        GameManager.player.equippedWeapon = weapon;
        Weapons.equippedWeapons[0] = weapon;
        Debug.Log("Picked up Weapon");
        // Drop Weapon
        pickup.GetComponent<WeaponPickup>().weapon = temp;

    }
}

