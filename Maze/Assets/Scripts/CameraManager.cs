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
        _camZoom.Zoom(_camera, 10f);
        if (_camZoom.curZoom != _camZoom.maxZoom) {
            _camMovement.EdgeScrolling(_camera, 40f, 10f, _camZoom.minZoom, _camZoom.maxZoom, _camZoom.curZoom);
        }
    }

    public void SetCameraPos(int width, int height) {
        Vector3 newPos = new Vector3(width - 1, 10, height - 1);
        _camera.transform.position = newPos;
        _camMovement.minVec = new Vector2(0,0);
        _camMovement.midVec = new Vector2(newPos.x, newPos.z);
        _camMovement.maxVec = new Vector2(width*2-2, height*2-2);
    }

    public void SetCameraSize(int width, int height) {
        _camZoom.minZoom = 1;
        _camZoom.maxZoom = width > height ? width : height;
        _camera.orthographicSize = _camZoom.maxZoom;
    }
}
