using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _minX;
    private float _minY;
    private float _maxX;
    private float _maxY;
    public float minX {
        get { return _minX; }
        set {
            _minX = value;
        }
    }
    public float minY {
        get { return _minY; }
        set {
            _minY = value;
        }
    }
    public float maxX {
        get { return _maxX; }
        set {
            _maxX = value;
        }
    }
    public float maxY {
        get { return _maxY; }
        set {
            _maxY = value;
        }
    }

    public void EdgeScrolling(Camera camera, float edgeSize, float moveAmount) {
        Vector3 newPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        if (Input.mousePosition.x > Screen.width - edgeSize) {
            newPos.x += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.x < edgeSize) {
            newPos.x -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize) {
            newPos.z += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y < edgeSize) {
            newPos.z -= moveAmount * Time.deltaTime;
        }
        camera.transform.position = newPos;
    }
}
