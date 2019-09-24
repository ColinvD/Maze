using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public enum WallDir {
        North,
        East,
        West,
        South
    }
    private Dictionary<WallDir, GameObject> walls;
    public List<WallDir> test;
    private int distance;
    private int gridX;
    private int gridY;
    private bool visited = false;
    public int Distance {
        get { return distance; }
        set {
            distance = value;
        }
    }
    public int GridX {
        get { return gridX; }
        set {
            gridX = value;
        }
    }
    public int GridY {
        get { return gridY; }
        set {
            gridY = value;
        }
    }
    public bool Visited {
        get { return visited; }
        set {
            visited = value;
        }
    }

    public void InitializeData() {
        walls = new Dictionary<WallDir, GameObject>();
        test = new List<WallDir>();
    }

    public void AddWall(WallDir direction, GameObject wall) {
        walls.Add(direction, wall);
        test.Add(direction);
    }

    public void RemoveWall(WallDir direction) {
        walls.Remove(direction);
        test.Remove(direction);
    }

    public bool ContainsWall(WallDir direction) {
        return walls.ContainsKey(direction);
    }

    public GameObject GetWall(WallDir direction) {
        return walls[direction];
    }
}
