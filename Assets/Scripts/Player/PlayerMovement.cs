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
            Move(transform.GetChild(0).forward* Time.deltaTime * speed * 2);
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

        Move(movement);

        if (movement != Vector3.zero)
        {
            Quaternion lookRotaion = Quaternion.LookRotation(movement, transform.up);

            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, lookRotaion, rotationSpeed * Time.deltaTime);
        }
        previousMovment = movement;
    }

    public void Move(Vector3 direction)
    {
        direction = CollisionCheck(direction);
        transform.position += direction;
    }

    Vector3 CollisionCheck(Vector3 dir)
    {
        Vector3 l = transform.position - Vector3.up * 0.5f;

        Ray ray = new Ray(l, dir);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2) && !hit.collider.isTrigger)
        {
            if (hit.distance < 0.4f)
            {
                Vector3 temp = Vector3.Cross(hit.normal, dir);
                Vector3 newDir = Vector3.Cross(temp, hit.normal);

                RaycastHit wallCheck = CheckWall(newDir);
                if (wallCheck.transform != null)
                {
                    newDir *= wallCheck.distance * 0.5f;
                }

                transform.position += newDir;
                return Vector3.zero;
            }
        }
        return dir;
    }

    RaycastHit CheckWall(Vector3 dir)
    {
        Vector3 l = transform.position - Vector3.up * 0.5f;
        Ray ray = new Ray(l, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.1f) && !hit.collider.isTrigger)
        {
            return hit;
        }
        return hit;
    }

}
