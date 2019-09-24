using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraPos(int width, int height) {
        Vector3 newPos = new Vector3(width - 1, 10, height - 1);
        camera.transform.position = newPos;
        camera.GetComponent<Camera>().orthographicSize = width > height ? width : height;
    }
}
