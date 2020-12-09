using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerPicker : MonoBehaviour {
    private Collider collider;
    private Collider foreignCollider;
    private InputManager inputManager;
    private Vector3 oldPos;
    private Quaternion oldRot;
    void Start() {
        collider = GetComponent<Collider>();
        foreignCollider = new Collider();
        inputManager = FindObjectOfType<InputManager>();

        oldPos = transform.position;
        oldRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (foreignCollider != null) {
            if (inputManager.triggerButtonPressed(XRNode.LeftHand) && foreignCollider.name.Contains("eft")) {
                Destroy(foreignCollider.transform.GetChild(1).GetChild(0));
                Instantiate(gameObject, Vector3.zero
                    , Quaternion.identity, foreignCollider.transform.GetChild(1).GetChild(0));
                //foreignCollider.transform.GetChild(1).GetChild(0).transform = transform;
            }

            if (inputManager.triggerButtonPressed(XRNode.RightHand) && foreignCollider.name.Contains("ight")) {
                Destroy(foreignCollider.transform.GetChild(1).GetChild(0));
                Instantiate(gameObject, Vector3.zero
                    , Quaternion.identity, foreignCollider.transform.GetChild(1).GetChild(0));
            }   
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Setting Collider");
        foreignCollider = other;
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Setting Collider");
        foreignCollider = null;
    }
}
