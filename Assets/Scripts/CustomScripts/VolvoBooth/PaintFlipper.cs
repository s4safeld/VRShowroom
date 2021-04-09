using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaintFlipper : MonoBehaviour {
    public bool forward;
    public Material[] materials;
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
    public PaintApply paintApply;
    static int i = 0;
    bool calledBefore = false;
    // Start is called before the first frame update
    void Start() {
        tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update() {

        if (tvButton.selected && MyInputManager.TriggerButtonPressed(tvButton.handIndicator)) {
            if (forward) {
                if (i >= materials.Length) i = 0;
                ChangePaint(materials[i++]);
            }
            else {
                if (i < 0) i = materials.Length - 1;
                ChangePaint(materials[i--]);
            }
        }
        else {
            calledBefore = false;
        }
    }

    private void ChangePaint(Material mat) {
        if(calledBefore) return;
        calledBefore = true;
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

        paintApply.currentMat = mat;
    }
}
