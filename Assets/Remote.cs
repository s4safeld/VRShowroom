using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour {
    public bool grabbed = false;
    private MyInputManager mim;
    public Transform currHand;
    public Vector3 holdingPosition;

    void Start() {
        mim = FindObjectOfType<MyInputManager>();
    }

    void Update() {

        if (grabbed) {
            Debug.Log("grabbed");
        }

        if (!grabbed && mim.GripValue('l') > 0) {
            if (Vector3.Distance(transform.position, mim.leftHandController.position) < 1) {
                grabbed = true;
                transform.Rotate(holdingPosition);
                currHand = mim.leftHandController;
            }
        }

        if (!grabbed && mim.GripValue('r') > 0) {
            if (Vector3.Distance(transform.position, mim.rightHandController.position) < 1) {
                grabbed = true;
                transform.Rotate(holdingPosition);
                currHand = mim.rightHandController;
            }
        }

        if (grabbed) {
            if (currHand == mim.rightHandController) {
                if (!mim.TriggerButtonPressed('r'))
                    grabbed = false;
            }
            else {
                if (!mim.TriggerButtonPressed('l'))
                    grabbed = false;
            }
        }
    }
}
