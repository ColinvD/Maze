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
    private GridGenerator generator;

    // Start is called before the first frame update
    void Start()
    {
        generator = FindObjectOfType<GridGenerator>();
    }

    public void Generate() {
        generator.GenerateMaze(int.Parse(widthInput.text), int.Parse(heightInput.text));
    }
}
