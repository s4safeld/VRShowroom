using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using UnityEngine.Serialization;

public class Tool : MonoBehaviour {
    public bool grabbed = false;
    public float grabbingDistance = 1;
    public GameObject descriptionTexts;
    public Outline outline;

    private Transform oldParent;
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    public char currHandIndicator = 'r';
    private bool enableDescriptionTexts;
    private Vector2 prim2DAxis;

    private void Start() {
        oldParent = transform.parent;
        oldPosition = transform.position;
        oldRotation = transform.rotation;
    }

    void Update() {
        prim2DAxis = MyInputManager.Primary2DAxis('r');
        
        if (!grabbed && Vector3.Distance(transform.position, MyInputManager.rightHandController.position) < grabbingDistance) {
            outline.enabled = true;
            if(MyInputManager.TriggerValue('r') > 0.5f) {
                outline.enabled = false;
                grabbed = true;
                foreach (var mr in MyInputManager.rightHandController.GetComponentsInChildren<MeshRenderer>()) {
                    mr.enabled = false;
                }

                enableDescriptionTextsFor(5);

                transform.parent = MyInputManager.rightHandController.parent;
                transform.rotation = MyInputManager.rightHandController.rotation;
                transform.position = MyInputManager.rightHandController.position;

                currHandIndicator = 'r';
            }
        }
        else {
            outline.enabled = false;
        }
        
        if (grabbed) {
            if (prim2DAxis.x <  0.3 && prim2DAxis.y <  0.3 &&
                prim2DAxis.x > -0.3 && prim2DAxis.y > -0.3 && 
                MyInputManager.Primary2DAxisClick('r')) {
                transform.parent = oldParent;
                transform.rotation = oldRotation;
                transform.position = oldPosition;

                if (currHandIndicator == 'r') {
                    foreach (var mr in MyInputManager.rightHandController.GetComponentsInChildren<MeshRenderer>()) {
                        mr.enabled = true;
                    }
                }
                    

                grabbed = false;
                //outline.enabled = false;
            }
        }

        
        if (!enableDescriptionTexts) {
            if (MyInputManager.GripValue('r') > 0) {
                foreach (var meshRenderer in descriptionTexts.GetComponentsInChildren<MeshRenderer>()) {
                    meshRenderer.enabled = true;
                }
            }
            else {
                foreach (var meshRenderer in descriptionTexts.GetComponentsInChildren<MeshRenderer>()) {
                    meshRenderer.enabled = false;
                }
            }
        } 
    }

    IEnumerator enableDescriptionTextsFor(float time) {
        foreach (var meshRenderer in descriptionTexts.GetComponentsInChildren<MeshRenderer>()) {
            meshRenderer.enabled = true;
        }
        enableDescriptionTexts = true;
        yield return new WaitForSeconds(time);
        enableDescriptionTexts = false;
    }

}
