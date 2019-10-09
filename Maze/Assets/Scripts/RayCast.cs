using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public RaycastHit SendRaycast(Transform origin) {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.down), out hit, Mathf.Infinity)) {
            Debug.DrawRay(origin.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        } else {
            Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.down) * 1000, Color.white);
        }
        return hit;
    }
}
