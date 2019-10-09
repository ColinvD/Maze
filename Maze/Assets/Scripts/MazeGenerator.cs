using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private GridGenerator _generator;
    private RoomData _curChecking;
    private RoomData _startRoom;
    private RoomData _endRoom;
    private List<RoomData> _path;
    private List<RoomData> _outside;
    private int _distance = 0;
    private int _currentX = 0;
    private int _currentY = 0;
    private float _animSpeed = 0.3f;
    public delegate void Generated();
    public Generated mazeGenerated;
    public Transform StartroomPos {
        get { return _startRoom.transform; }
    }
    
    void Start()
    {
        _generator = FindObjectOfType<GridGenerator>();
    }

    private void MakeEnds() { // maakt de start en einde van de maze
        _endRoom = null;
        while (_endRoom == null) {
            RoomData temp = _startRoom;
            for (int i = 0; i < _outside.Count; i++) {
                if (_outside[i].Distance >= temp.Distance) {
                    temp = _outside[i];
                }
            }
            if (temp != _startRoom) {
                _endRoom = temp;
            }
        }
        _startRoom.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        _endRoom.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        _endRoom.gameObject.tag = "Finish";
        mazeGenerated();
    }

    private void GetRandomStart() { // pakt een random start kamer van de buitenrand
        _outside = new List<RoomData>();
        for (int i = 0; i < _generator.Width; i++) {
            for (int j = 0; j < _generator.Height; j++) {
                if (i == 0 || i == _generator.Width - 1 || j == 0 || j == _generator.Height - 1) {
                    _outside.Add(_generator.Grid[i, j]);
                    if (i == 0) {
                        _generator.Grid[i, j].RemoveWall(RoomData.WallDir.West);
                    }
                    if (i == _generator.Width - 1) {
                        _generator.Grid[i, j].RemoveWall(RoomData.WallDir.East);
                    }
                    if (j == 0) {
                        _generator.Grid[i, j].RemoveWall(RoomData.WallDir.South);
                    }
                    if (j == _generator.Height - 1) {
                        _generator.Grid[i, j].RemoveWall(RoomData.WallDir.North);
                    }
                }
            }
        }
        _curChecking = _outside[Random.Range(0, _outside.Count - 1)];
        for (int i = 0; i < _generator.Width; i++) {
            for (int j = 0; j < _generator.Height; j++) {
                if (_generator.Grid[i,j] == _curChecking) {
                    _currentX = i;
                    _currentY = j;
                }
            }
        }
        _curChecking.Visited = true;
        _curChecking.Distance = _distance;
        _path.Add(_curChecking);
        _startRoom = _curChecking;
    }

    public IEnumerator GenerateMazeAnim() { // maakt de maze in een soort van animatie
        _path = new List<RoomData>();
        GetRandomStart();
        while (_path.Count > 0) {
            _currentX = _curChecking.GridX;
            _currentY = _curChecking.GridY;
            List<RoomData.WallDir> directions = new List<RoomData.WallDir>(); 
            for (int i = 0; i < 4; i++) {
                if (_curChecking.ContainsWall((RoomData.WallDir)i)) {
                    directions.Add((RoomData.WallDir)i);
                }
            }
            if (directions.Count != 0) {
                ChangeColor(_curChecking, Color.yellow);
                CheckRoom(directions[GetRandomDir(directions.Count)]);
            } else if (_path.Count - 1 > 0) {
                ChangeColor(_curChecking, Color.white);
                _distance--;
                _path.Remove(_path[_path.Count - 1]);
                _curChecking = _path[_path.Count - 1];
                ChangeColor(_curChecking, Color.yellow);
            } else {
                ChangeColor(_curChecking, Color.white);
                break;
            }
            yield return new WaitForSeconds(_animSpeed);
        }
        MakeEnds();
    }

    public void GenerateMaze() { // maakt de maze in 1 keer
        _path = new List<RoomData>();
        GetRandomStart();
        while (_path.Count > 0) {
            _currentX = _curChecking.GridX;
            _currentY = _curChecking.GridY;
            List<RoomData.WallDir> directions = new List<RoomData.WallDir>();
            for (int i = 0; i < 4; i++) {
                if (_curChecking.ContainsWall((RoomData.WallDir)i)) {
                    directions.Add((RoomData.WallDir)i);
                }
            }
            if (directions.Count != 0) {
                ChangeColor(_curChecking, Color.yellow);
                CheckRoom(directions[GetRandomDir(directions.Count)]);
            } else if (_path.Count - 1 > 0) {
                ChangeColor(_curChecking, Color.white);
                _distance--;
                _path.Remove(_path[_path.Count - 1]);
                _curChecking = _path[_path.Count - 1];
                ChangeColor(_curChecking, Color.yellow);
            } else {
                ChangeColor(_curChecking, Color.white);
                break;
            }
        }
        MakeEnds();
    }

    private void CheckRoom(RoomData.WallDir direction) { // controleert als we wel die richting op kunnen
        switch (direction) {
            case RoomData.WallDir.North:
                if (!_generator.Grid[_currentX,_currentY + 1].Visited) {
                    ChangeColor(_curChecking, Color.magenta);
                    Destroy(_curChecking.GetWall(direction));
                    _curChecking.RemoveWall(direction);
                    _generator.Grid[_currentX, _currentY + 1].RemoveWall(RoomData.WallDir.South);
                    _path.Add(_generator.Grid[_currentX, _currentY + 1]);
                    _curChecking = _generator.Grid[_currentX, _currentY + 1];
                    ChangeColor(_curChecking, Color.yellow);
                    _curChecking.Visited = true;
                    _distance++;
                    _curChecking.Distance = _distance;
                } else {
                    _curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.East:
                if (!_generator.Grid[_currentX + 1, _currentY].Visited) {
                    ChangeColor(_curChecking, Color.magenta);
                    Destroy(_curChecking.GetWall(direction));
                    _curChecking.RemoveWall(direction);
                    _generator.Grid[_currentX + 1, _currentY].RemoveWall(RoomData.WallDir.West);
                    _path.Add(_generator.Grid[_currentX + 1, _currentY]);
                    _curChecking = _generator.Grid[_currentX + 1, _currentY];
                    ChangeColor(_curChecking, Color.yellow);
                    _curChecking.Visited = true;
                    _distance++;
                    _curChecking.Distance = _distance;
                } else {
                    _curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.South:
                if (!_generator.Grid[_currentX, _currentY - 1].Visited) {
                    ChangeColor(_curChecking, Color.magenta);
                    Destroy(_curChecking.GetWall(direction));
                    _curChecking.RemoveWall(direction);
                    _generator.Grid[_currentX, _currentY - 1].RemoveWall(RoomData.WallDir.North);
                    _path.Add(_generator.Grid[_currentX, _currentY - 1]);
                    _curChecking = _generator.Grid[_currentX, _currentY - 1];
                    ChangeColor(_curChecking, Color.yellow);
                    _curChecking.Visited = true;
                    _distance++;
                    _curChecking.Distance = _distance;
                } else {
                    _curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.West:
                if (!_generator.Grid[_currentX - 1, _currentY].Visited) {
                    ChangeColor(_curChecking, Color.magenta);
                    Destroy(_curChecking.GetWall(direction));
                    _curChecking.RemoveWall(direction);
                    _generator.Grid[_currentX - 1, _currentY].RemoveWall(RoomData.WallDir.East);
                    _path.Add(_generator.Grid[_currentX - 1, _currentY]);
                    _curChecking = _generator.Grid[_currentX - 1, _currentY];
                    ChangeColor(_curChecking, Color.yellow);
                    _curChecking.Visited = true;
                    _distance++;
                    _curChecking.Distance = _distance;
                } else {
                    _curChecking.RemoveWall(direction);
                }
                break;
        }
    }

    private int GetRandomDir(int maxRange) { // geeft een random richting terug
        return Random.Range(0, maxRange);
    }

    private void ChangeColor(RoomData room, Color color) { // veranderd de kamer kleur
        room.gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
    
    public void DestroyMaze(Transform mazeParent) { // vernietig de vorige maze
        foreach (Transform child in mazeParent) {
            Destroy(child.gameObject);
        }
    }

    /* #1 get a random starting room
     * #2 choose a random direction from that room
     * #3 check the room in that direction if it has been visited
     * #4 if yes
     *      - check other directions
     *      - if there are other rooms
     *          -remove wall between rooms
     *          -add room in the path list
     *          -check that room right away (goes to #2)
     *      - if no other rooms
     *          - remove this room from path list
     *          - go back one room and check other directions (goes to #2)
     *    if no
     *      - add room in the path list
     *      -check that room right away (goes to #2)
     * #5 if path is empty (so all rooms have been checked) then open the outside wall of the first room
     * #6 grab a random outside room that has a higher distance then half of the other rooms their distances
     * #7 open the outside wall of that room
     */
}
