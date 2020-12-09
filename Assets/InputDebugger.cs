using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class InputDebugger : MonoBehaviour {
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start() {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (inputManager.triggerButtonPressed(XRNode.LeftHand)) {
            Debug.Log("left Trigger pressed");
        }

        if (inputManager.thumbrestPressed(XRNode.LeftHand)) {
            Debug.Log("left Thumb Rest pressed");
        }

        if (inputManager.gripValue(XRNode.LeftHand) != 0.0f) {
            Debug.Log("left gripped pressed with value: " + inputManager.gripValue(XRNode.LeftHand));
        }

        if (inputManager.triggerButtonPressed(XRNode.RightHand)) {
            Debug.Log("Right Trigger pressed");
        }

        if (inputManager.thumbrestPressed(XRNode.RightHand)) {
            Debug.Log("Right Thumb Rest pressed");
        }

        if (inputManager.gripValue(XRNode.RightHand) != 0.0f) {
            Debug.Log("Right gripped pressed with value: " + inputManager.gripValue(XRNode.RightHand));
        }
        */
    }
}
