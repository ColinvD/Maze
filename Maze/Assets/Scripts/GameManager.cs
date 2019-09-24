using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridGenerator _gridGen;
    private MazeGenerator _mazeGen;
    private UIHandler _uihandler;
    private CameraSetter _camSetter;
    
    void Start()
    {
        _gridGen = GetComponent<GridGenerator>();
        _mazeGen = GetComponent<MazeGenerator>();
        _uihandler = GetComponent<UIHandler>();
        _camSetter = GetComponent<CameraSetter>();
    }
    
    void Update() {
        _mazeGen.SetSpeed(_uihandler.GetSpeed());
    }

    public void Generate() { // maakt de volledige maze
        StopCoroutine(_mazeGen.GenerateMazeAnim());
        _gridGen.GenerateGrid(_uihandler.GetWidthInput(), _uihandler.GetHeightInput());
        _camSetter.SetCameraPos(_gridGen.Width, _gridGen.Height);
        if (_uihandler.GetAnimation()) {
            StartCoroutine(_mazeGen.GenerateMazeAnim());
        } else {
            _uihandler.SetSpeed(1);
            _mazeGen.GenerateMaze();
        }
        _mazeGen.SetSpeed(1);
    }
}
