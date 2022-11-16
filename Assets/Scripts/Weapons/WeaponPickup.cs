using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [HideInInspector]
    public Weapon weapon;
    public string weaponName;
    public int damage;
    public int accuracy;
    public float reloadSpeed;
    public Weapons.WEAPONTYPE weaponType;
    public int ammoCapacity;
    public float RoF;

    private void Start()
    {
        weapon = new Weapon(weaponName, damage, accuracy, ammoCapacity, reloadSpeed, RoF, weaponType);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 0.5f);
    }
}
