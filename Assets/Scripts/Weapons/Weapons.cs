using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Weapons
{
    static public List<Weapon> weaponList = new List<Weapon>();

    static public Weapon[] equippedWeapons = new Weapon[2];



    public enum WEAPONTYPE { Sniper, Autorifle, Pistol, Shotgun };




    static public void Initialize()
    {
        weaponList.Add(new Weapon("Auto Rifle", 16, 75, 30, 2.5f, 0.15f, WEAPONTYPE.Autorifle));
        weaponList.Add(new Weapon("Pistol", 6, 95, 6, 1.5f, 0.25f, WEAPONTYPE.Pistol));
        weaponList.Add(new Weapon("Sniper Rifle", 50, 100, 5, 4, 0.5f, WEAPONTYPE.Sniper));
        
        

        equippedWeapons[0] = weaponList[0];
        equippedWeapons[1] = weaponList[1];
    }

    static public void Update()
    {
        equippedWeapons[0].Update();
    }

    static public void SwapWeapons()
    {
        Weapon temp = equippedWeapons[0];
        equippedWeapons[0] = equippedWeapons[1];
        equippedWeapons[1] = temp;

        Debug.Log(equippedWeapons[0].name);
        Debug.Log(equippedWeapons[1].name);
    }
}
