using System;
using System.Collections;
using System.Collections.Generic;
using Data.Util;
using Unity.Mathematics;
using UnityEngine;

public class RobotControlSphere : MonoBehaviour {
    public Transform target;
    private char handIndicator = 'n';
    private bool isGrabbed = false;

    public Transform[] claws;
    public Vector3[] clawsRotationGoals;
    public Transform grababbleObjectsParent;
    private Collider[] grababbleObjects;
    public Transform[] clawTips;
    private Vector3[] clawsRotationOrigins;

    private float triggerValueOfPreviousFrame = 0;
    private Outline outline;


    private void Start() {
        outline = GetComponent<Outline>();

        grababbleObjects = grababbleObjectsParent.GetComponentsInChildren<Collider>();
        
        clawsRotationOrigins = new Vector3[claws.Length];
        for (int i = 0; i < claws.Length; i++) {
            clawsRotationOrigins[i] = claws[i].localEulerAngles;
        }
    }

    void Update() {
        if (transform.localPosition.x > 3 || transform.localPosition.y > 3 || transform.localPosition.z > 3 ||
            transform.localPosition.x < -3 || transform.localPosition.y < -3 || transform.localPosition.z < -3)
            transform.localPosition = Vector3.up;
        
        var position = new Vector3(transform.position.x+7.96f, transform.position.y-5.57f, transform.position.z-1.18f);
        
        target.localPosition = new Vector3(position.x*3, position.y, -position.z*3);
        target.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        handIndicator = MyInputManager.InRange(transform.position, 0.5f);


        if (handIndicator != '0') {
            if (!isGrabbed)
                outline.enabled = true;
            
            if (MyInputManager.GripValue(handIndicator) > 0 && !isGrabbed) {
                isGrabbed = true;
                outline.enabled = false;
            }

            if (MyInputManager.GripValue(handIndicator) > 0 && !isGrabbed) {
                isGrabbed = true;
                outline.enabled = false;
            }
            
            MoveClaws(MyInputManager.TriggerValue(handIndicator));
            
            if (isGrabbed && MyInputManager.GripValue(handIndicator) <= 0)
                isGrabbed = false;
        }
    }

    void MoveClaws(float input) {

        if (input < triggerValueOfPreviousFrame || input <= 0) {
            foreach (var obj in grababbleObjects) {
                obj.transform.parent = grababbleObjectsParent;
                obj.GetComponent<Rigidbody>().useGravity = true;
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            for (var i = 0; i < claws.Length; i++) {
                claws[i].localRotation = Quaternion.Lerp(Quaternion.Euler(clawsRotationOrigins[i])
                    , Quaternion.Euler(clawsRotationGoals[i]), input);
            }
        }
        else {
            for (var i = 0; i < claws.Length; i++) {
                foreach (Collider col in grababbleObjects) {
                    if (col.bounds.Contains(clawTips[i].position)) {
                        col.transform.parent = clawTips[i];
                        col.GetComponent<Rigidbody>().useGravity = false;
                        col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        return;
                    }
                }
                claws[i].localRotation = Quaternion.Lerp(Quaternion.Euler(clawsRotationOrigins[i])
                    , Quaternion.Euler(clawsRotationGoals[i]), input);
            }
        }

        triggerValueOfPreviousFrame = input;
    }
}
