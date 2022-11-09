using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStuff : MonoBehaviour
{
    GameObject fadedObject;
    
    Color fadedObjectColor;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    GameObject target;

    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.Normalize(target.transform.position - transform.position), out hit, 16.0f, (1 << 9)))
        {
            if (fadedObject == null)
            {
                fadedObject = hit.transform.gameObject;
                fadedObjectColor = fadedObject.GetComponent<MeshRenderer>().material.color;
                fadedObject.GetComponent<MeshRenderer>().material.color -= new Color(0, 0, 0, 0.25f);
            }
            else if (fadedObject != hit.transform.gameObject)
            {
                fadedObject.GetComponent<MeshRenderer>().material.color = fadedObjectColor;
                fadedObject = hit.transform.gameObject;
                fadedObjectColor = fadedObject.GetComponent<MeshRenderer>().material.color;
                fadedObject.GetComponent<MeshRenderer>().material.color -= new Color(0, 0, 0, 0.25f);
            }
        }
        else if (fadedObject != null)
        {
            fadedObject.GetComponent<MeshRenderer>().material.color = fadedObjectColor;
            fadedObject = null;
        }
        
    }
}
