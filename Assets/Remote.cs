using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class Remote : MonoBehaviour {
    public bool grabbed = false;
    private MyInputManager mim;
    public Transform currHand;
    public Vector3 holdingPosition;
    public float grabbingDistance = 1;
    public SphereCollider controllerHolster;

    private float rotationFunctionSpeedFromParent = 0;

    void Start() {
        mim = FindObjectOfType<MyInputManager>();
    }

    void Update() {

        //if(mim.GripValue('l')>0)
        //    Debug.Log(mim.GripValue('l'));
        //if(mim.GripValue('r')>0)
        //    Debug.Log(mim.GripValue('r'));
        //Debug.Log(name+", grabbed: "+grabbed);

        if (!grabbed && mim.GripValue('l') > 0) {
            if (Vector3.Distance(transform.position, mim.leftHandController.position) < grabbingDistance) {
                Debug.Log("here");
                grabbed = true;
                transform.eulerAngles = holdingPosition;
                currHand = mim.leftHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
                ToggleRotationFunctionalityFromParent();
            }
        }

        if (!grabbed && mim.GripValue('r') > 0) {
            if (Vector3.Distance(transform.position, mim.rightHandController.position) < grabbingDistance) {
                grabbed = true;
                transform.eulerAngles = holdingPosition;
                currHand = mim.rightHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
                ToggleRotationFunctionalityFromParent();
            }
        }

        if (grabbed) {
            if (currHand == mim.rightHandController) {
                if (mim.GripValue('r') == 0) {
                    grabbed = false;
                    ToggleRotationFunctionalityFromParent();
                    if (Vector3.Distance(controllerHolster.center, transform.position) < controllerHolster.radius) {
                        controllerHolster.GetComponent<AudioSource>().Play();
                        ControllerStorage.storage.Add(gameObject);
                        enabled = false;
                    }

                    currHand.GetComponent<myController>().grabbedSomething = false;
                    currHand = null;
                }
                    
            }
            if(currHand == mim.leftHandController){
                if (mim.GripValue('l') == 0) {
                    grabbed = false; 
                    ToggleRotationFunctionalityFromParent();
                    if (Vector3.Distance(controllerHolster.center, transform.position) < controllerHolster.radius) {
                        controllerHolster.GetComponent<AudioSource>().Play();
                        ControllerStorage.storage.Add(gameObject);
                        enabled = false;
                    }

                    currHand.GetComponent<myController>().grabbedSomething = false;
                    currHand = null;
                }
            }
        }
    }

    void ToggleRotationFunctionalityFromParent() {
        try {
            XRRayInteractor xrRayInteractor = GetComponentInParent<XRRayInteractor>();
            if (xrRayInteractor.rotateSpeed > 0) {
                if (rotationFunctionSpeedFromParent == 0)
                    rotationFunctionSpeedFromParent = xrRayInteractor.rotateSpeed;
                xrRayInteractor.rotateSpeed = 0;
            }
            else {
                xrRayInteractor.rotateSpeed = rotationFunctionSpeedFromParent;
            }
        }
        catch (Exception) { return;}
    }
    
}
