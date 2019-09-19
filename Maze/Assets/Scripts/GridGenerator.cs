using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private int width = 5;
    private int height = 5;
    private GameObject[,] grid;

    public int Width {
        get { return width; }
        set {
            width = value;
        }
    }
    public int Height {
        get { return height; }
        set {
            height = value;
        }
    }
    public GameObject[,] Grid {
        get { return grid; }
        private set {
            grid = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Grid = new GameObject[width,height];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
