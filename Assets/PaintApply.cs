using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintApply : MonoBehaviour
{
    public Renderer body;
    public int bodyIndex;
    public Renderer doorFrLeft;
    public int doorFrLeftIndex;
    public Renderer doorFrRight;
    public int doorFrRightIndex;
    public Renderer doorRearLeft;
    public int doorRearLeftIndex;
    public Renderer doorRearRight;
    public int doorRearRightIndex;
    private TVButton tvButton;

    public Material currentMat;
    
    bool calledBefore = false;
    // Start is called before the first frame update
    void Start() {
        tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tvButton.selected && MyInputManager.TriggerButtonPressed(tvButton.handIndicator))
            ApplyPaint(currentMat);

        if (!tvButton.selected || !MyInputManager.TriggerButtonPressed(tvButton.handIndicator)) {
            calledBefore = false;
        }
    }

    void ApplyPaint(Material mat) {
        if (calledBefore) return;
        
        Material[] mats = body.materials;
        mats[bodyIndex] = mat;
        body.materials = mats;

        mats = doorFrLeft.materials;
        mats[doorFrLeftIndex] = mat;
        doorFrLeft.materials = mats;

        mats = doorFrRight.materials;
        mats[doorFrRightIndex] = mat;
        doorFrRight.materials = mats;

        mats = doorRearLeft.materials;
        mats[doorRearLeftIndex] = mat;
        doorRearLeft.materials = mats;

        mats = doorRearRight.materials;
        mats[doorRearRightIndex] = mat;
        doorRearRight.materials = mats;

        calledBefore = true;
    }
}
