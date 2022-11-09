using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 5;
    public float speed = 10;
    public int damage = 20;
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        life -= Time.deltaTime;
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.gameObject.layer == 8 || other.gameObject.layer == 10)
        {
            Debug.Log("An entity was hit");
            Debug.Log(other.name);
            other.gameObject.GetComponent<CoreEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
