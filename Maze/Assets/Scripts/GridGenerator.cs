using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private int _width = 5;
    private int _height = 5;
    private RoomData[,] _grid;
    [SerializeField]
    private GameObject _room;
    [SerializeField]
    private GameObject _wall;
    [SerializeField]
    private Transform _mazeParent;
    private GameObject[,] _horizontalWalls;
    private GameObject[,] _verticalWalls;

    public int Width {
        get { return _width; }
        set {
            _width = value;
        }
    }
    public int Height {
        get { return _height; }
        set {
            _height = value;
        }
    }
    public RoomData[,] Grid {
        get { return _grid; }
        private set {
            _grid = value;
        }
    }

    private void DestroyMaze() { // vernietig de vorige maze
        foreach (Transform child in _mazeParent) {
            Destroy(child.gameObject);
        }
    }

    public void GenerateGrid(int newWidth, int newHeight) { // maakt de hele grid aan
        Width = newWidth;
        Height = newHeight;
        Grid = new RoomData[_width, _height];
        _horizontalWalls = new GameObject[_width, _height + 1];
        _verticalWalls = new GameObject[_width + 1, _height];
        SpawnRooms();
        SpawnWalls();
        InitializeRooms();
    }

    private void SpawnRooms() { // spawnt de fysieke kamers in de wereld
        for (int i = 0; i < _width; i++) {
            for (int j = 0; j < _height; j++) {
                Vector3 pos = new Vector3(i * 2, 0, j * 2);
                GameObject mazeRoom = Instantiate(_room, pos, new Quaternion(), _mazeParent);
                mazeRoom.name = "" + i + j;
                _grid[i, j] = mazeRoom.GetComponent<RoomData>();
                _grid[i, j].GridX = i;
                _grid[i, j].GridY = j;
            }
        }
    }

    private void SpawnWalls() { // spawnt de fysieke muren in de wereld
        // horizontal
        for (int i = 0; i < _width; i++) {
            for (int j = 0; j <= _height; j++) {
                Vector3 pos = new Vector3(i * 2, 0, -1f + j * 2);
                GameObject mazeWall = Instantiate(_wall, pos, new Quaternion(), _mazeParent);
                mazeWall.transform.localScale = new Vector3(2.5f, 1, 0.5f);
                _horizontalWalls[i,j] = mazeWall;
            }
        }

        // vertical
        for (int i = 0; i <= _width; i++) {
            for (int j = 0; j < _height; j++) {
                Vector3 pos = new Vector3(-1f + i * 2, 0, j * 2);
                GameObject mazeWall = Instantiate(_wall, pos, new Quaternion(), _mazeParent);
                mazeWall.transform.localScale = new Vector3(0.5f, 1, 2.5f);
                _verticalWalls[i, j] = mazeWall;
            }
        }
    }

    private void InitializeRooms() { // stelt voor elke kamer 4 muren in
        for (int i = 0; i < _width; i++) {
            for (int j = 0; j < _height; j++) {
                RoomData data = _grid[i, j].GetComponent<RoomData>();
                data.InitializeData();
                data.AddWall(RoomData.WallDir.North, _horizontalWalls[i, j + 1]);
                data.AddWall(RoomData.WallDir.East, _verticalWalls[i+1, j]);
                data.AddWall(RoomData.WallDir.South, _horizontalWalls[i, j]);
                data.AddWall(RoomData.WallDir.West, _verticalWalls[i, j]);
            }
        }
    }
}
