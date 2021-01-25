using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;

public class MyInputManager : MonoBehaviour
{
    private XRNode leftHand = XRNode.LeftHand;
    private XRNode rightHand = XRNode.RightHand;
    
    private List<InputDevice> devicesLeft = new List<InputDevice>();
    private List<InputDevice> devicesRight = new List<InputDevice>();

    public InputDevice DeviceLeft;
    public InputDevice DeviceRight;

    public Transform leftHandController;
    public Transform rightHandController;

    private void GetLeftDevice() {
        InputDevices.GetDevicesAtXRNode(leftHand, devicesLeft);
        DeviceLeft = devicesLeft.FirstOrDefault();

    }
    private void GetRightDevice() {
        InputDevices.GetDevicesAtXRNode(rightHand, devicesRight);
        DeviceRight = devicesRight.FirstOrDefault();
    }
    
    private void OnEnable() {
        if (!DeviceLeft.isValid) {
            GetLeftDevice();
        }
        if (!DeviceRight.isValid) {
            GetRightDevice();
        }
    }

    private void Update() {
        if (!DeviceLeft.isValid) {
            GetLeftDevice();
        }

        if (!DeviceRight.isValid) {
            GetRightDevice();
        }

        #if UNITY_ANDROID
            Debug.Log("on android platform");
        #endif
        
        #if !UNITY_ANDROID
            Debug.Log("not on Android Platform");
        #endif
        
    }
    
    //consult the following link to see what each Button does for each Platform
    //https://docs.unity3d.com/Manual/xr_input.html
    //Not every function is supported on each platform

    public bool TriggerButtonPressed(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value);
            }
            default:
                Debug.LogError("Something went wrong in InputManager.triggerButtonPressed(), controller seems to be neither right or left");
                return false;
        }
    }

    public float GripValue(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.grip, out float value) ? value : 0.0f;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.grip, out float value) ? value : 0.0f;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.gripValue(), controller seems to be neither right or left");
                return 0.0f;
        }
    }

    public bool ThumbrestPressed(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.thumbrest, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.thumbrest, out bool value) && value);
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.thumbrestPressed(), controller seems to be neither right nor left");
                return false;
        }
    }

    public float TriggerValue(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.trigger, out float value) ? value : 0.0f;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.trigger, out float value) ? value : 0.0f;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.gripValue(), controller seems to be neither right nor left");
                return 0.0f;
        }
    }

    public float BatteryLevel(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.batteryLevel, out float value) ? value : 0.0f;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.batteryLevel, out float value) ? value : 0.0f;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.BatteryLevel(), controller seems to be neither right nor left");
                return 0.0f;
        }
    }

    public Vector3 DeviceAcceleration(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 value) ? value : Vector3.zero;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 value) ? value : Vector3.zero;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.BatteryLevel(), controller seems to be neither right nor left");
                return Vector3.zero;
        }
    }

    public bool MenuButton(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.menuButton, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.menuButton, out bool value) && value);
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.menuButton(), controller seems to be neither right nor left");
                return false;
        }
    }

    public Vector2 Primary2DAxis(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value)
                    ? value
                    : Vector2.zero;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value)
                    ? value
                    : Vector2.zero;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.BatteryLevel(), controller seems to be neither right nor left");
                return Vector2.zero;
        }
    }

    public bool Primary2DAxisTouch(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value) && value);
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.Primary2DAxisTouch(), controller seems to be neither right nor left");
                return false;
        }
    }

    public bool Primary2DAxisClick(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value) && value);
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.Primary2DAxisClick(), controller seems to be neither right nor left");
                return false;
        }
    }

    public Vector2 dPad(char hand) {
        switch (hand) {
            case 'l': {
                return DeviceLeft.TryGetFeatureValue(CommonUsages.dPad, out Vector2 value)
                    ? value
                    : Vector2.zero;
            }
            case 'r': {
                return DeviceRight.TryGetFeatureValue(CommonUsages.dPad, out Vector2 value)
                    ? value
                    : Vector2.zero;
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.dPad(), controller seems to be neither right nor left");
                return Vector2.zero;
        }
    }

    public bool PrimaryButton(char hand) {
        switch (hand) {
            case 'l': {
                return (DeviceLeft.TryGetFeatureValue(CommonUsages.primaryButton, out bool value) && value);
            }
            case 'r': {
                return (DeviceRight.TryGetFeatureValue(CommonUsages.primaryButton, out bool value) && value);
            }
            default:
                Debug.LogError(
                    "Something went wrong in InputManager.PrimaryButton, controller seems to be neither right nor left");
                return false;
        }
    }
}
