using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using UnityEngine.Serialization;

public class Remote : MonoBehaviour {
    public bool grabbed = false;
    public float grabbingDistance = 1;

    private Transform oldParent;
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    public char currHandIndicator;

    private void Start() {
        oldParent = transform.parent;
        oldPosition = transform.position;
        oldRotation = transform.rotation;
    }

    void Update() {
        if (!grabbed && MyInputManager.GripValue('l') > 0) {
            if (Vector3.Distance(transform.position, MyInputManager.leftHandController.position) < grabbingDistance) {
                grabbed = true;
                foreach (var mr in MyInputManager.leftHandController.GetComponentsInChildren<MeshRenderer>()) {
                    mr.enabled = false;
                }

                transform.parent = MyInputManager.leftHandController.parent;
                transform.rotation = MyInputManager.leftHandController.rotation;
                transform.position = MyInputManager.leftHandController.position;

                currHandIndicator = 'l';
            }
        }
        else 
        if(!grabbed && MyInputManager.GripValue('r') > 0) {
            if (Vector3.Distance(transform.position, MyInputManager.rightHandController.position) < grabbingDistance) {
                grabbed = true;
                foreach (var mr in MyInputManager.rightHandController.GetComponentsInChildren<MeshRenderer>()) {
                    mr.enabled = false;
                }

                transform.parent = MyInputManager.rightHandController.parent;
                transform.rotation = MyInputManager.rightHandController.rotation;
                transform.position = MyInputManager.rightHandController.position;

                currHandIndicator = 'r';
            }
        }

        

        if (grabbed) {
            if (MyInputManager.GripValue(currHandIndicator) <= 0) {
                transform.parent = oldParent;
                transform.rotation = oldRotation;
                transform.position = oldPosition;

                if (currHandIndicator == 'l') 
                    foreach (var mr in MyInputManager.leftHandController.GetComponentsInChildren<MeshRenderer>()) 
                        mr.enabled = true;
                else
                    foreach (var mr in MyInputManager.rightHandController.GetComponentsInChildren<MeshRenderer>())
                        mr.enabled = true;

                grabbed = false;
            }
        }
    }

}
