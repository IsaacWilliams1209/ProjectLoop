using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{

    public string name;
    public int damage;
    public int accuracy;
    public int ammoUsed;
    public float reloadSpeed;
    public Weapons.WEAPONTYPE weaponType;
    public bool isEquipped = false;
    public bool isReloading = false;
    public int ammoCapacity;
    public float RoF;
    float[] timers = new float[2] { 0, 0 }; // List of timers: 0. Reload timer; 1. Rate of Fire-

    public Weapon(string weaponName, int weaponDamage, int weaponAccuracy, int ammo, float reloadSpeed, float rateOfFire, Weapons.WEAPONTYPE wEAPONTYPE)
    {
        ammoUsed = 0;
        name = weaponName;
        damage = weaponDamage;
        accuracy = weaponAccuracy;
        ammoCapacity = ammo;
        RoF = rateOfFire;
        this.reloadSpeed = reloadSpeed;
        weaponType = wEAPONTYPE;
    }

    public Weapon(Weapon baseWeapon)
    {
        ammoUsed = 0;
        name = baseWeapon.name;
        damage = baseWeapon.damage;
        accuracy = baseWeapon.accuracy;
        ammoCapacity = baseWeapon.ammoCapacity;
        RoF = baseWeapon.RoF;
        reloadSpeed = baseWeapon.reloadSpeed;
        weaponType = baseWeapon.weaponType;
    }

    public void Fire(Vector3 position, Vector3 direction)
    {
        if (ammoUsed == ammoCapacity)
        {
            Reload();
            return;
        }
        if (timers[1] <= 0)
        {           

            Vector3 shotDir = (direction - position).normalized;

            Debug.DrawRay(position, shotDir, Color.white, 0.5f);

            shotDir = ApplyAccuracy(shotDir);

            Quaternion temp = Quaternion.LookRotation(shotDir);
            GameObject bullet = Object.Instantiate(GameManager.bullet, position + shotDir * 0.75f, temp);
            bullet.GetComponent<Bullet>().damage = damage;
            ammoUsed++;
            timers[1] = RoF;
        }        
    }

    public void Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            timers[0] = reloadSpeed;
        }
    }

    Vector3 ApplyAccuracy(Vector3 desiredVector)
    {
        Vector3 newDirection = Quaternion.AngleAxis(Random.Range(-(100 - accuracy) / 2, (100 - accuracy) / 2), Vector3.up) * desiredVector;

        return newDirection;
    }

    public void Update()
    {
        for (int i = 0; i < timers.Length; i++)
        {
            if (timers[i] <= 0)
            {
                timers[i] = 0;
            }            
            else
            {
                timers[i] -= Time.deltaTime;
            }
        }
        if (timers[0] == 0 && isReloading)
        {
                isReloading = false;
                ammoUsed = 0;
        }
    }
}
