using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private InputField _widthInput;
    [SerializeField]
    private InputField _heightInput;
    [SerializeField]
    private Toggle _mazeAnimation;
    [SerializeField]
    private Slider _speed;

    public int GetWidthInput() {
        return int.Parse(_widthInput.text);
    }

    public int GetHeightInput() {
        return int.Parse(_heightInput.text);
    }

    public bool GetAnimation() {
        return _mazeAnimation.isOn;
    }

    public float GetSpeed() {
        return _speed.value;
    }

    public void SetSpeed(float newSpeed) {
        _speed.value = newSpeed;
    }
}
