using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Attack standardAttack = new Attack();

    [SerializeField]
    float range;

    Vector3 checkBox = new Vector3();

    Transform trueFacing;

    public Weapon equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        trueFacing = transform.GetChild(0);
        equippedWeapon = Weapons.equippedWeapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !equippedWeapon.isReloading && (equippedWeapon.ammoCapacity != equippedWeapon.ammoUsed))
        {
            Weapons.equippedWeapons[0].Reload();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !equippedWeapon.isReloading)
        {
            equippedWeapon.Fire(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Weapons.SwapWeapons();
            equippedWeapon = Weapons.equippedWeapons[0];
        }
    }
}
