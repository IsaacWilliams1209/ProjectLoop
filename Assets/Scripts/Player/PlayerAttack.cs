using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Entity
{

    [SerializeField]
    float range;

    public Weapon equippedWeapon;

    

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = Weapons.equippedWeapons[0];
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<PlayerMovement>().isInvulnerable)
            return;
        if (Input.GetKeyDown(KeyCode.R) && !equippedWeapon.isReloading && (equippedWeapon.ammoCapacity != equippedWeapon.ammoUsed))
        {
            Weapons.equippedWeapons[0].Reload();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !equippedWeapon.isReloading)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 9));

            Vector3 point = hit.point;
            point.y = transform.position.y;
            equippedWeapon.Fire(transform.position, point);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Weapons.SwapWeapons();
            equippedWeapon = Weapons.equippedWeapons[0];
        }
    }

    public override void TakeDamage(int damage)
    {
        if (gameObject.GetComponent<PlayerMovement>().isInvulnerable)
            return;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Application.Quit();
            // Die
            // Drop Loot
        }
    }

}
