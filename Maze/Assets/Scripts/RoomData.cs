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
    private int distance;
    private bool visited = false;
    public int Distance {
        get { return distance; }
        set {
            distance = value;
        }
    }
    public bool Visited {
        get { return visited; }
        set {
            visited = value;
        }
    }

    public void AddWall(WallDir direction, GameObject wall) {
        Debug.Log("Dir: " + direction + " wall: " + wall);
        walls.Add(direction, wall);
    }
}
