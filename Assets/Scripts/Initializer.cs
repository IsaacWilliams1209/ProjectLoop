using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject bullet;
    public PlayerAttack player;


    private void Start()
    {
        GameManager.bullet = bullet;
        GameManager.rand = new Random();
        GameManager.player = GameObject.Find("Player").GetComponent<PlayerAttack>();
        Weapons.Initialize();
    }

    private void Update()
    {
        Weapons.Update();
    }
}
