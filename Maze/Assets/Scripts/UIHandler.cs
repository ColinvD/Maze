using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private InputField widthInput;
    [SerializeField]
    private InputField heightInput;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public int GetWidthInput() {
        return int.Parse(widthInput.text);
    }

    public int GetHeightInput() {
        return int.Parse(heightInput.text);
    }
}
