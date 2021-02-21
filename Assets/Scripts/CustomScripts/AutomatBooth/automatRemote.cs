using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class automatRemote : MonoBehaviour {

    private Remote remote;
    private MyInputManager mim;
    public GameObject device;
    
    private void Start() {
        remote = GetComponent<Remote>();
        mim = FindObjectOfType<MyInputManager>();
    }

    public void Update() {
        if (remote.grabbed) {
            if (remote.currHand == mim.leftHandController) {
                if (mim.Primary2DAxisClick('l')) {
                    device.GetComponent<automat>().explode();
                }
            }

            if (remote.currHand == mim.rightHandController) {
                if (mim.Primary2DAxisClick('r')) {
                    device.GetComponent<automat>().explode();
                }
            }
        }
    }

    /*public bool grabbed = false;
    public GameObject device;
    private MyInputManager mim;
    public GameObject leftHand;
    public GameObject rightHand;
    private GameObject currHand;
    // Start is called before the first frame update
    public void Fire()
    {
        if(grabbed)
            device.GetComponent<automat>().explode();
    }

    void Start() {
        mim = FindObjectOfType<MyInputManager>();
    }

    void Update() {

        if (grabbed) {
            Debug.Log("grabbed");
        }

        if (!grabbed && mim.GripValue('l')>0) {
            if (Vector3.Distance(transform.position, leftHand.transform.position) < 1) {
                Debug.Log("here");
                grabbed = true;
                transform.localRotation = leftHand.transform.rotation;
                currHand = leftHand;
            }
        }

        if (!grabbed && mim.GripValue('r')>0) {
            if (Vector3.Distance(transform.position, rightHand.transform.position) < 1) {
                Debug.Log("here");
                grabbed = true;
                transform.localRotation = rightHand.transform.rotation;
                currHand = rightHand;
            }
        }

        if (grabbed) {
            if (currHand == rightHand) {

                if (mim.Primary2DAxisClick('r'))
                    Fire();

                if (!mim.TriggerButtonPressed('r'))
                    grabbed = false;
                
            }
            else {

                if (mim.Primary2DAxisClick('l'))
                    Fire();
                
                if (!mim.TriggerButtonPressed('l'))
                    grabbed = false;
                
            }
        }

    }
    */
}
