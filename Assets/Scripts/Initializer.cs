using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject bullet;
    //public PlayerAttack player;
    [HideInInspector]
    public RandomGen randomGen;


    private void Start()
    {
        //GameManager.bullet = bullet;
        //GameManager.player = GameObject.Find("Player").GetComponent<PlayerAttack>();
        //Weapons.Initialize();
        randomGen = new RandomGen();
        randomGen.GenerateRooms();
        
    }

    private void Update()
    {
        //Weapons.Update();
    }
}
