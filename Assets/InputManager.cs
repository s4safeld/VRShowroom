using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;

public class InputManager : MonoBehaviour {
    private XRNode leftHand = XRNode.LeftHand;
    private XRNode rightHand = XRNode.RightHand;
    
    private List<InputDevice> devicesLeft = new List<InputDevice>();
    private List<InputDevice> devicesRight = new List<InputDevice>();

    public InputDevice deviceLeft;
    public InputDevice deviceRight;

    private void GetLeftDevice() {
        InputDevices.GetDevicesAtXRNode(leftHand, devicesLeft);
        deviceLeft = devicesLeft.FirstOrDefault();

    }
    private void GetRightDevice() {
        InputDevices.GetDevicesAtXRNode(rightHand, devicesRight);
        deviceRight = devicesRight.FirstOrDefault();
    }
    

    private void OnEnable() {
        if (!deviceLeft.isValid) {
            GetLeftDevice();
        }
        if (!deviceRight.isValid) {
            GetRightDevice();
        }
    }

    private void Update() {
        if (!deviceLeft.isValid) {
            GetLeftDevice();
        }

        if (!deviceRight.isValid) {
            GetRightDevice();
        }
    }

    public bool triggerButtonPressed(XRNode controller) {
        if (controller == XRNode.LeftHand) {
            return (deviceLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value);
        }
        if (controller == XRNode.RightHand) {
            return (deviceRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value);   
        }
        Debug.LogError("Something went wrong in InputManager.triggerButtonPressed(), controller seems to be neither right nor left");
        return false;
    }

    public float gripValue(XRNode controller) {
        switch (controller) {
            case XRNode.LeftHand: {
                return deviceLeft.TryGetFeatureValue(CommonUsages.grip, out float value) ? value : 0.0f;
            }
            case XRNode.RightHand: {
                return deviceRight.TryGetFeatureValue(CommonUsages.grip, out float value) ? value : 0.0f;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.gripPressed(), controller seems to be neither right nor left");
                return 0.0f;
        }
    }

    public bool thumbrestPressed(XRNode controller) {
        if (controller == XRNode.LeftHand) {
            return (deviceLeft.TryGetFeatureValue(CommonUsages.thumbrest, out bool value) && value);
        }

        if (controller == XRNode.RightHand) {
            return (deviceRight.TryGetFeatureValue(CommonUsages.thumbrest, out bool value) && value);
        }

        Debug.LogError(
            "Something went wrong in InputManager.thumbrestPressed(), controller seems to be neither right nor left");
        return false;
    }

}
