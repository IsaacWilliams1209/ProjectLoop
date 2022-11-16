using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : Entity
{

    [SerializeField]
    float range;

    public Weapon equippedWeapon;

    public GameObject healthDisplay;

    public bool isArmoured;

    float[] timer = new float[2]; // List of timers: 0. Out of Combat Checker; 1. Health Regeneration;

    bool outOfCombat;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = Weapons.equippedWeapons[0];
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        timer[0] -= Time.deltaTime;
        if (timer[0] <= 0)
        {
            outOfCombat = true;
            timer[0] = 0;
        }
        if (outOfCombat)
        {
            RegenerateHealth();
        }
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
        healthDisplay.GetComponent<Text>().text = currentHealth.ToString();
    }

    public override void TakeDamage(int damage)
    {        
        if (gameObject.GetComponent<PlayerMovement>().isInvulnerable)
            return;
        timer[0] = 1f;
        if (isArmoured)
            damage /= 2;
        currentHealth -= damage;
        

        if (currentHealth <= 0)
        {
            Application.Quit();
            // Die
            // Drop Loot
        }
    }

    void RegenerateHealth()
    {
        if (currentHealth % 20 == 0)
            return;
        timer[1] += Time.deltaTime;
        if( timer[1] >= 0.5f)
        {
            timer[1] -= 0.5f;
            currentHealth++;
        }
    }
}
