using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [HideInInspector]
    public Weapon weapon;
    public int weaponIndex;

    private void Start()
    {
        weapon = new Weapon(Weapons.weaponList[weaponIndex]);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 0.5f);
    }
}
