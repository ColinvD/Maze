using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    private GameObject _player;
    public GameObject Player {
        get { return _player; }
        private set {
            _player = value;
        }
    }

    public void SpawnPlayer(Transform position) {
        Vector3 pos = new Vector3(position.position.x, position.position.y + 0.5f, position.position.z);
        position.position = pos;
        Player = Instantiate(_playerPrefab, position.position, position.rotation);
    }

    public void DestroyPlayer() {
        Destroy(Player);
    }
}
