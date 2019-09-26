using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement2D();
    }

    private void Movement2D() {
        Vector3 rotation = new Vector3();
        if (Input.GetAxis("Vertical") > 0) {
            if (Input.GetAxis("Horizontal") > 0) {
                rotation.y = 45;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            } else if (Input.GetAxis("Horizontal") < 0) {
                rotation.y = 315;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            } else {
                rotation.y = 0;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            }
        } else if (Input.GetAxis("Vertical") < 0) {
            if (Input.GetAxis("Horizontal") > 0) {
                rotation.y = 135;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            } else if (Input.GetAxis("Horizontal") < 0) {
                rotation.y = 225;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            } else {
                rotation.y = 180;
                transform.eulerAngles = rotation;
                gameObject.transform.position += transform.forward * _moveSpeed;
            }
        } else if (Input.GetAxis("Horizontal") > 0) {
            rotation.y = 90;
            transform.eulerAngles = rotation;
            gameObject.transform.position += transform.forward * _moveSpeed;
        } else if (Input.GetAxis("Horizontal") < 0) {
            rotation.y = 270;
            transform.eulerAngles = rotation;
            gameObject.transform.position += transform.forward * _moveSpeed;
        }
    }
}
