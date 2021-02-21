using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using UnityEngine.Serialization;

public class Remote : MonoBehaviour {
    public bool grabbed = false;
    private MyInputManager _mim;
    public Transform currHand;
    public float grabbingDistance = 1;
    public ControllerHolster controllerHolster;

    void Start() {
        _mim = FindObjectOfType<MyInputManager>();
    }

    void Update() {

        //if(mim.GripValue('l')>0)
        //    Debug.Log(mim.GripValue('l'));
        //if(mim.GripValue('r')>0)
        //    Debug.Log(mim.GripValue('r'));
        //Debug.Log(name+", grabbed: "+grabbed);

        if (!grabbed && _mim.GripValue('l') > 0) {
            if (Vector3.Distance(transform.position, _mim.leftHandController.position) < grabbingDistance) {
                grabbed = true;
                currHand = _mim.leftHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
            }
        }

        if (!grabbed && _mim.GripValue('r') > 0) {
            if (Vector3.Distance(transform.position, _mim.rightHandController.position) < grabbingDistance) {
                grabbed = true;
                currHand = _mim.rightHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
            }
        }

        if (grabbed) {

            transform.rotation = currHand.rotation;

            if (currHand == _mim.rightHandController) {
                if (_mim.GripValue('r') == 0) {
                    grabbed = false;
                    /*if (controllerHolster.isInside(GetComponent<Collider>())) {
                        controllerHolster.GetComponent<AudioSource>().Play();
                        ControllerStorage.storage.Add(gameObject);
                        transform.position -= new Vector3(0,-100,0);
                    }*/

                    currHand.GetComponent<myController>().grabbedSomething = false;
                    currHand = null;
                }
                    
            }
            if(currHand == _mim.leftHandController){
                if (_mim.GripValue('l') == 0) {
                    grabbed = false;
                    /*if (controllerHolster.isInside(GetComponent<Collider>())) {
                        controllerHolster.GetComponent<AudioSource>().Play();
                        ControllerStorage.storage.Add(gameObject);
                        transform.position -= new Vector3(0, -100, 0);
                    }*/

                    currHand.GetComponent<myController>().grabbedSomething = false;
                    currHand = null;
                }
            }
        }
    }

}
