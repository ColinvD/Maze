using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private int width = 5;
    private int height = 5;
    private GameObject[,] grid;
    [SerializeField]
    private GameObject room;
    private GameObject[,] horizontalWalls;
    private GameObject[,] verticalWalls;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private Transform mazeParent;

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
        horizontalWalls = new GameObject[width, height+1];
        verticalWalls = new GameObject[width + 1, height];
        SpawnRooms();
        SpawnWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRooms() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Vector3 pos = new Vector3(i * 2, 0, j * 2);
                GameObject mazeRoom = Instantiate(room, pos, new Quaternion(), mazeParent);
                grid[i, j] = mazeRoom;
            }
        }
    }

    private void SpawnWalls() {
        //horizontal
        for (int i = 0; i < width; i++) {
            for (int j = 0; j <= height; j++) {
                Vector3 pos = new Vector3(i * 2, 0, -1f + j * 2);
                GameObject mazeWall = Instantiate(wall, pos, new Quaternion(), mazeParent);
                mazeWall.transform.localScale = new Vector3(2.5f, 1, 0.5f);
                horizontalWalls[i,j] = mazeWall;
            }
        }

        //vertical
        for (int i = 0; i <= width; i++) {
            for (int j = 0; j < height; j++) {
                Vector3 pos = new Vector3(-1f + i * 2, 0, j * 2);
                GameObject mazeWall = Instantiate(wall, pos, new Quaternion(), mazeParent);
                mazeWall.transform.localScale = new Vector3(0.5f, 1, 2.5f);
                verticalWalls[i, j] = mazeWall;
            }
        }
    }
}
