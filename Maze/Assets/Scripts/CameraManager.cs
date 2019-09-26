using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private CameraMovement _camMovement;
    private CameraZoom _camZoom;

    void Start() {
        _camMovement = GetComponent<CameraMovement>();
        _camZoom = GetComponent<CameraZoom>();
    }

    void Update() {
        _camMovement.EdgeScrolling(_camera, 40f, 10f);
        _camZoom.Zoom(_camera, 10f);
    }

    public void SetCameraPos(int width, int height) {
        Vector3 newPos = new Vector3(width - 1, 10, height - 1);
        _camera.transform.position = newPos;
    }

    public void SetCameraSize(int width, int height) {
        _camZoom.minZoom = 1;
        _camZoom.maxZoom = width > height ? width : height;
        _camera.orthographicSize = _camZoom.maxZoom;
    }
}
