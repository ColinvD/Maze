using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    public void SetCameraPos(int width, int height) {
        Vector3 newPos = new Vector3(width - 1, 10, height - 1);
        _camera.transform.position = newPos;
        _camera.orthographicSize = width > height ? width : height;
    }
}
