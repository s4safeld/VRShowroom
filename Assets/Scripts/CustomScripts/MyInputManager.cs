using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MyInputManager : MonoBehaviour
{
    private XRNode leftHand = XRNode.LeftHand;
    private XRNode rightHand = XRNode.RightHand;
    
    private List<InputDevice> devicesLeft = new List<InputDevice>();
    private List<InputDevice> devicesRight = new List<InputDevice>();

    public static InputDevice DeviceLeft;
    public static InputDevice DeviceRight;

    public Transform setLeftHandController;
    public Transform setRightHandController;

    public static Transform leftHandController;
    public static Transform rightHandController;

    public GameObject oculusleftControllerModel;
    public GameObject oculusRightControllerModel;
    public GameObject viveLeftControllerModel;
    public GameObject viveRightControllerModel;

    public XRRayInteractor setLeftHandRayInteractor;
    public XRRayInteractor setRightHandRayInteractor;
    
    public static XRRayInteractor leftHandRayInteractor;
    public static XRRayInteractor rightHandRayInteractor;

    private MeshRenderer[] leftDescriptions;
    private MeshRenderer[] rightDescriptions;


    private void OnEnable() {
        if (!DeviceLeft.isValid) {
            GetLeftDevice();
        }
        if (!DeviceRight.isValid) {
            GetRightDevice();
        }

        leftHandController = setLeftHandController;
        rightHandController = setRightHandController;

        leftHandRayInteractor = setLeftHandRayInteractor;
        rightHandRayInteractor = setRightHandRayInteractor;
        
        #if UNITY_ANDROID
        Instantiate(oculusleftControllerModel, leftHandController.position, leftHandController.rotation, leftHandController);
        Instantiate(oculusRightControllerModel, rightHandController.position, rightHandController.rotation, rightHandController);
        #else
        Instantiate(viveLeftControllerModel, leftHandController.position, leftHandController.rotation, leftHandController);
        Instantiate(viveRightControllerModel, rightHandController.position, rightHandController.rotation, rightHandController);
        #endif

        leftDescriptions = leftHandController.GetChild(0).GetChild(0).GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < leftDescriptions.Length; i++) {
            leftDescriptions[i].enabled = false;
        }

        rightDescriptions = rightHandController.GetChild(0).GetChild(0).GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < rightDescriptions.Length; i++) {
            rightDescriptions[i].enabled = false;
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
            //Debug.Log("on android platform");
        #endif

        #if !UNITY_ANDROID 
            //Debug.Log("not on Android Platform");
        #endif

        if (MyInputManager.GripValue('l') > 0) {
            for (int i = 0; i < leftDescriptions.Length; i++) {
                leftDescriptions[i].enabled = true;
            }
            for (int i = 0; i < rightDescriptions.Length; i++) {
                rightDescriptions[i].enabled = true;
            }
        }
        else {
            for (int i = 0; i < leftDescriptions.Length; i++) {
                leftDescriptions[i].enabled = false;
            }

            for (int i = 0; i < rightDescriptions.Length; i++) {
                rightDescriptions[i].enabled = false;
            }
        }
    }
    
    public static char InRange(Vector3 input, float grabbingRange) {
        if (Vector3.Distance(leftHandController.position, input) < grabbingRange &&
            Vector3.Distance(leftHandController.position, input) <= Vector3.Distance(rightHandController.position, input))
            return 'l';
        if (Vector3.Distance(rightHandController.position, input) < grabbingRange &&
            Vector3.Distance(rightHandController.position, input) <= Vector3.Distance(leftHandController.position, input))
            return 'r';
        return '0';
    }

    public static char HoveredByRayInteractor(Collider col) {
        try {
            if (leftHandRayInteractor.enabled) {
                if (leftHandRayInteractor.GetCurrentRaycastHit(out RaycastHit raycastHit)) {
                    if (raycastHit.collider == col) {
                        return 'l';
                    }
                }
            }
        }catch { /*ignored*/ }
        try {
            if (rightHandRayInteractor.enabled) {
                if (rightHandRayInteractor.GetCurrentRaycastHit(out RaycastHit raycastHit)) {
                    if (raycastHit.collider == col) {
                        return 'r';
                    }
                }
            }
        }catch { /*ignored*/ }
        return '0';
    }

    private void GetLeftDevice() {
        InputDevices.GetDevicesAtXRNode(leftHand, devicesLeft);
        DeviceLeft = devicesLeft.FirstOrDefault();
    }
    
    private void GetRightDevice() {
        InputDevices.GetDevicesAtXRNode(rightHand, devicesRight);
        DeviceRight = devicesRight.FirstOrDefault();
    }
    
    
    
    //consult the following link to see what each Button does for each Platform
    //https://docs.unity3d.com/Manual/xr_input.html
    //Not every function is supported on each platform

    public static bool TriggerButtonPressed(char hand) {
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

    public static float GripValue(char hand) {
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

    public static float TriggerValue(char hand) {
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

    public static float BatteryLevel(char hand) {
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

    public static Vector3 DeviceAcceleration(char hand) {
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

    public static bool MenuButton(char hand) {
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

    public static Vector2 Primary2DAxis(char hand) {
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

    public static bool Primary2DAxisTouch(char hand) {
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

    public static bool Primary2DAxisClick(char hand) {
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

    public static bool PrimaryButton(char hand) {
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
