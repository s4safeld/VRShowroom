using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class numPadButton : MonoBehaviour
{
    public int value = 0;
    public bool isEnter = false;
    public bool isUndo = false;
    public TextMeshPro screenText;
    public VIPArea vipArea = null;
    private TVButton _tvButton;

    private bool isSelected = false;
    // Start is called before the first frame update
    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tvButton.selected 
            && (MyInputManager.TriggerButtonPressed('l') || MyInputManager.TriggerButtonPressed('r')) 
            && !isSelected) {
            
            isSelected = true;
            if (!isEnter && !isUndo && screenText.text.Length<4)
                screenText.text += value;
            if (isEnter && !isUndo && vipArea)
                vipArea.allowAccess(screenText.text,true);
            if (!isEnter && isUndo && screenText.text.Length > 0)
                screenText.text = screenText.text.Remove(screenText.text.Length - 1);
        }

        if (!_tvButton.selected && isSelected) {
            isSelected = false;
        }
    }
}
