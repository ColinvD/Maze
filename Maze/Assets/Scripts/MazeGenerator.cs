using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private GridGenerator generator;
    private RoomData curChecking;
    private RoomData startRoom;
    private List<RoomData> path;
    private List<RoomData> outside;
    private int distance = 0;
    private int currentX = 0;
    private int currentY = 0;

    // Start is called before the first frame update
    void Start()
    {
        generator = FindObjectOfType<GridGenerator>();
    }

    public void MakeEnds() {
        List<RoomData> rankList = new List<RoomData>();
        rankList.Add(startRoom);
        for (int i = 0; i < outside.Count; i++) {
            for (int j = 0; j < rankList.Count; j++) {
                if (outside[i].Distance > rankList[j].Distance) {
                    rankList.Insert(j, outside[i]);
                } else {
                    rankList.Add(outside[i]);
                }
            }
        }
        startRoom.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        rankList[Random.Range(0, (int)rankList.Count / 2)].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void GetRandomStart() {
        outside = new List<RoomData>();
        for (int i = 0; i < generator.Width; i++) {
            for (int j = 0; j < generator.Height; j++) {
                if (i == 0 || i == generator.Width - 1 || j == 0 || j == generator.Height - 1) {
                    outside.Add(generator.Grid[i, j]);
                    if (i == 0) {
                        generator.Grid[i, j].RemoveWall(RoomData.WallDir.West);
                    }
                    if (i == generator.Width - 1) {
                        generator.Grid[i, j].RemoveWall(RoomData.WallDir.East);
                    }
                    if (j == 0) {
                        generator.Grid[i, j].RemoveWall(RoomData.WallDir.South);
                    }
                    if (j == generator.Height - 1) {
                        generator.Grid[i, j].RemoveWall(RoomData.WallDir.North);
                    }
                }
            }
        }
        curChecking = outside[Random.Range(0, outside.Count - 1)];
        for (int i = 0; i < generator.Width; i++) {
            for (int j = 0; j < generator.Height; j++) {
                if (generator.Grid[i,j] == curChecking) {
                    currentX = i;
                    currentY = j;
                }
            }
        }
        curChecking.Visited = true;
        curChecking.Distance = distance;
        path.Add(curChecking);
        startRoom = curChecking;
    }

    public void GenerateMaze() {
        path = new List<RoomData>();
        GetRandomStart(); //Get a random starting room
        while (path.Count > 0) {
            char[] name = curChecking.name.ToCharArray();
            currentX = (int)char.GetNumericValue(name[0]);
            currentY = (int)char.GetNumericValue(name[1]);
            List<RoomData.WallDir> directions = new List<RoomData.WallDir>(); //from my current room what directions can i go to
            for (int i = 0; i < 4; i++) {
                if (curChecking.ContainsWall((RoomData.WallDir)i)) { //als de kamer nog die muur heeft
                    directions.Add((RoomData.WallDir)i); //voeg hem toe aan welke richtingen ik nog kan checken
                }
            }
            if (directions.Count != 0) { //checkt als ik nog naar een direction kan
                CheckRoom(directions[GetRandomDir(directions.Count)]); //check die direction
            } else {
                path.Remove(path[path.Count - 1]); //haal deze kamer uit het pad
                curChecking = path[path.Count - 1]; //en ga naar de vorige kamer
            }
        }
        MakeEnds();
    }

    private void CheckRoom(RoomData.WallDir direction) {
        switch (direction) {
            case RoomData.WallDir.North:
                if (!generator.Grid[currentX,currentY + 1].Visited) { //als hij nog niet is bezocht
                    Destroy(curChecking.GetWall(direction)); //muur vernietigen
                    curChecking.RemoveWall(direction); //haal bij de huidige kamer de muur weg
                    generator.Grid[currentX, currentY + 1].RemoveWall(RoomData.WallDir.South); //haal bij de volgende kamer dezelfde muur weg
                    path.Add(generator.Grid[currentX, currentY + 1]); //voeg de volgende kamer toe aan het pad
                    curChecking = generator.Grid[currentX, currentY + 1]; //zet die kamer als de huidige die gecheckt word
                    curChecking.Visited = true;
                    distance++;
                    curChecking.Distance = distance;
                } else {
                    curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.East:
                if (!generator.Grid[currentX + 1, currentY].Visited) {
                    Destroy(curChecking.GetWall(direction));
                    curChecking.RemoveWall(direction);
                    generator.Grid[currentX + 1, currentY].RemoveWall(RoomData.WallDir.West);
                    path.Add(generator.Grid[currentX + 1, currentY]);
                    curChecking = generator.Grid[currentX + 1, currentY];
                    curChecking.Visited = true;
                    distance++;
                    curChecking.Distance = distance;
                } else {
                    curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.South:
                if (!generator.Grid[currentX, currentY - 1].Visited) {
                    Destroy(curChecking.GetWall(direction));
                    curChecking.RemoveWall(direction);
                    generator.Grid[currentX, currentY - 1].RemoveWall(RoomData.WallDir.North);
                    path.Add(generator.Grid[currentX, currentY - 1]);
                    curChecking = generator.Grid[currentX, currentY - 1];
                    curChecking.Visited = true;
                    distance++;
                    curChecking.Distance = distance;
                } else {
                    curChecking.RemoveWall(direction);
                }
                break;
            case RoomData.WallDir.West:
                if (!generator.Grid[currentX - 1, currentY].Visited) {
                    Destroy(curChecking.GetWall(direction));
                    curChecking.RemoveWall(direction);
                    generator.Grid[currentX - 1, currentY].RemoveWall(RoomData.WallDir.East);
                    path.Add(generator.Grid[currentX - 1, currentY]);
                    curChecking = generator.Grid[currentX - 1, currentY];
                    curChecking.Visited = true;
                    distance++;
                    curChecking.Distance = distance;
                } else {
                    curChecking.RemoveWall(direction);
                }
                break;
        }
    }

    private int GetRandomDir(int maxRange) {
        return Random.Range(0, maxRange);
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
