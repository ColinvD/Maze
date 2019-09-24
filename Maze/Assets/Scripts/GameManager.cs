using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridGenerator gridGen;
    private MazeGenerator mazeGen;
    private UIHandler uihandler;
    private CameraSetter camSetter;

    // Start is called before the first frame update
    void Start()
    {
        gridGen = GetComponent<GridGenerator>();
        mazeGen = GetComponent<MazeGenerator>();
        uihandler = GetComponent<UIHandler>();
        camSetter = GetComponent<CameraSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate() {
        gridGen.GenerateMaze(uihandler.GetWidthInput(), uihandler.GetHeightInput());
        camSetter.SetCameraPos(gridGen.Width, gridGen.Height);
        //StartCoroutine(mazeGen.GenerateMazeAnim());
        mazeGen.GenerateMaze();
    }
}
