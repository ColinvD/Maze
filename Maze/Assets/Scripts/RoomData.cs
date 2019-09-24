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
    private Dictionary<WallDir, GameObject> _walls;
    private int _distance;
    private int _gridX;
    private int _gridY;
    private bool _visited = false;
    public int Distance {
        get { return _distance; }
        set {
            _distance = value;
        }
    }
    public int GridX {
        get { return _gridX; }
        set {
            _gridX = value;
        }
    }
    public int GridY {
        get { return _gridY; }
        set {
            _gridY = value;
        }
    }
    public bool Visited {
        get { return _visited; }
        set {
            _visited = value;
        }
    }

    public void InitializeData() {
        _walls = new Dictionary<WallDir, GameObject>();
    }

    public void AddWall(WallDir direction, GameObject wall) {
        _walls.Add(direction, wall);
    }

    public void RemoveWall(WallDir direction) {
        _walls.Remove(direction);
    }

    public bool ContainsWall(WallDir direction) {
        return _walls.ContainsKey(direction);
    }

    public GameObject GetWall(WallDir direction) {
        return _walls[direction];
    }
}
