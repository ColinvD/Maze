using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridGenerator _gridGen;
    private MazeGenerator _mazeGen;
    private UIHandler _uihandler;
    private CameraManager _camSetter;
    private PlayerSpawner _playerSpawner;
    private RayCast _raycast;
    [SerializeField]
    private GameObject _winPanel;
    private bool _generating = false;
    private bool _victory = false;
    
    void Start()
    {
        _gridGen = GetComponent<GridGenerator>();
        _mazeGen = GetComponent<MazeGenerator>();
        _uihandler = GetComponent<UIHandler>();
        _camSetter = GetComponent<CameraManager>();
        _playerSpawner = GetComponent<PlayerSpawner>();
        _raycast = GetComponent<RayCast>();
        _mazeGen.mazeGenerated += PlayGame;
    }
    
    void Update() {
        if (_generating) {
            SetSpeed(_uihandler.GetSpeed());
        }
        if (_playerSpawner.Player != null) {
            RaycastHit hit = _raycast.SendRaycast(_playerSpawner.Player.transform);
            if (hit.collider.tag == "Finish" && _victory == false) {
                _victory = true;
                StartCoroutine(WinShow());
            }
        }
    }

    public void Generate() { // maakt de volledige maze
        StopCoroutine(_mazeGen.GenerateMazeAnim());
        _mazeGen.DestroyMaze(_gridGen.MazeParent);
        _playerSpawner.DestroyPlayer();
        _victory = false;
        _gridGen.GenerateGrid(_uihandler.GetWidthInput(), _uihandler.GetHeightInput());
        _camSetter.SetCameraPos(_gridGen.Width, _gridGen.Height);
        _camSetter.SetCameraSize(_gridGen.Width, _gridGen.Height);
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

    private void ActiveSetter(GameObject gameobject, bool newValue) {
        gameobject.SetActive(newValue);
    }

    private IEnumerator WinShow() {
        _winPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        _winPanel.SetActive(false);
    }
}
