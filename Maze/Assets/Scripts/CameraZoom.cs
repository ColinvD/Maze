using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float _minZoom;
    private float _maxZoom;
    public float minZoom {
        get { return _minZoom; }
        set {
            _minZoom = value;
        }
    }
    public float maxZoom {
        get { return _maxZoom; }
        set {
            _maxZoom = value;
        }
    }

    public void Zoom(Camera camera, float zoomSpeed) {
        float zoom = camera.orthographicSize;
        if (Input.mouseScrollDelta.y > 0) {
            zoom -= zoomSpeed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0) {
            zoom += zoomSpeed * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        camera.orthographicSize = zoom;
    }
}
