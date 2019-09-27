using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 _minVec;
    private Vector2 _maxVec;
    private Vector2 _midVec;
    public Vector2 minVec {
        get { return _minVec; }
        set {
            _minVec = value;
        }
    }
    public Vector2 maxVec {
        get { return _maxVec; }
        set {
            _maxVec = value;
        }
    }
    public Vector2 midVec {
        get { return _midVec; }
        set {
            _midVec = value;
        }
    }

    public void EdgeScrolling(Camera camera, float edgeSize, float moveAmount, float sizeMin, float sizeMax, float sizeCur) {
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
        float tempX = minVec.x - midVec.x;
        tempX *= 1 - ((sizeCur - sizeMin) / (sizeMax - sizeMin));
        float tempY = minVec.y - midVec.y;
        tempY *= 1 - ((sizeCur - sizeMin) / (sizeMax - sizeMin));
        newPos.x = Mathf.Clamp(newPos.x, midVec.x + tempX, midVec.x - tempX);
        newPos.z = Mathf.Clamp(newPos.z, midVec.y + tempY, midVec.y - tempY);
        camera.transform.position = newPos;
    }
}
