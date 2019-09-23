using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridGenerator gridGen;
    private MazeGenerator mazeGen;
    private UIHandler uihandler;
    // Start is called before the first frame update
    void Start()
    {
        gridGen = FindObjectOfType<GridGenerator>();
        mazeGen = FindObjectOfType<MazeGenerator>();
        uihandler = FindObjectOfType<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate() {
        gridGen.GenerateMaze(uihandler.GetWidthInput(), uihandler.GetHeightInput());
        mazeGen.GenerateMaze();
    }
}
