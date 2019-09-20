using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private GridGenerator generator;
    private RoomData startPos;
    private List<RoomData> path;

    // Start is called before the first frame update
    void Start()
    {
        generator = FindObjectOfType<GridGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetRandomStart() {
        List<RoomData> outside = new List<RoomData>();
        for (int i = 0; i < generator.Width; i++) {
            for (int j = 0; j < generator.Height; j++) {
                if (i == 0 || i == generator.Width - 1 || j == 0 || j == generator.Height - 1) {
                    outside.Add(generator.Grid[i, j]);
                }
            }
        }
        startPos = outside[Random.Range(0, outside.Count - 1)];
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
