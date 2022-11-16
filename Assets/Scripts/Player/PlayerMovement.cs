using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    GameObject mesh;

    [SerializeField]
    CharacterController controller;

    Vector3 previousMovment;

    public bool isInvulnerable;

    int iFrames;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && timer < 0)
        {
            // Roll
            if (!isInvulnerable)
            {
                isInvulnerable = true;
                iFrames = 120;
            }
        }
        if (iFrames > 0)
        {
            iFrames--;
            controller.Move(transform.GetChild(0).forward* Time.deltaTime * speed * 2);
            return;
        }
        else if (iFrames == 0)
        {
            iFrames--;
            timer = 0.5f; 
        }
        else
        {
            isInvulnerable = false;
        }
        Vector3 movement = Vector3.zero;

        movement += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        movement += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        controller.Move(movement);

        if (movement != Vector3.zero)
        {
            Quaternion lookRotaion = Quaternion.LookRotation(movement, transform.up);

            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, lookRotaion, rotationSpeed * Time.deltaTime);
        }
        previousMovment = movement;
    }

}
