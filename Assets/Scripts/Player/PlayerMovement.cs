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

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        movement += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        movement += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        controller.Move(movement);

        if (movement != Vector3.zero)
        {
            Quaternion lookRotaion = Quaternion.LookRotation(movement, transform.up);

            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, lookRotaion, rotationSpeed * Time.deltaTime);
        }
    }

}
