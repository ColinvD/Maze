using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridGenerator _gridGen;
    private MazeGenerator _mazeGen;
    private UIHandler _uihandler;
    private CameraSetter _camSetter;
    private PlayerSpawner _playerSpawner;
    private bool _generating = false;
    
    void Start()
    {
        _gridGen = GetComponent<GridGenerator>();
        _mazeGen = GetComponent<MazeGenerator>();
        _uihandler = GetComponent<UIHandler>();
        _camSetter = GetComponent<CameraSetter>();
        _playerSpawner = GetComponent<PlayerSpawner>();
        _mazeGen.mazeGenerated += PlayGame;
    }
    
    void Update() {
        if (_generating) {
            SetSpeed(_uihandler.GetSpeed());
        }
    }

    public void Generate() { // maakt de volledige maze
        StopCoroutine(_mazeGen.GenerateMazeAnim());
        _mazeGen.DestroyMaze(_gridGen.MazeParent);
        _playerSpawner.DestroyPlayer();
        _gridGen.GenerateGrid(_uihandler.GetWidthInput(), _uihandler.GetHeightInput());
        _camSetter.SetCameraPos(_gridGen.Width, _gridGen.Height);
        if (_uihandler.GetAnimation()) {
            _generating = true;
            StartCoroutine(_mazeGen.GenerateMazeAnim());
        } else {
            _uihandler.SetSpeed(1);
            _generating = false;
            _mazeGen.GenerateMaze();
        }
    }

    private void PlayGame() {
        SetSpeed(1);
        _generating = false;
        _playerSpawner.SpawnPlayer(_mazeGen.StartroomPos);
    }

    public void SetSpeed(float newSpeed) { // zet de snelheid van de animatie
        Time.timeScale = newSpeed;
    }
}
