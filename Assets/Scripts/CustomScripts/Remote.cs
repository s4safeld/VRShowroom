using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using UnityEngine.Serialization;

public class Remote : MonoBehaviour {
    public bool grabbed = false;
    public Transform currHand;
    public float grabbingDistance = 1;

    void Update() {

        //if(mim.GripValue('l')>0)
        //    Debug.Log(mim.GripValue('l'));
        //if(mim.GripValue('r')>0)
        //    Debug.Log(mim.GripValue('r'));
        //Debug.Log(name+", grabbed: "+grabbed);

        if (!grabbed && MyInputManager.GripValue('l') > 0) {
            if (Vector3.Distance(transform.position, MyInputManager.leftHandController.position) < grabbingDistance) {
                grabbed = true;
                currHand = MyInputManager.leftHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
            }
        }

        if (!grabbed && MyInputManager.GripValue('r') > 0) {
            if (Vector3.Distance(transform.position, MyInputManager.rightHandController.position) < grabbingDistance) {
                grabbed = true;
                currHand = MyInputManager.rightHandController;
                currHand.GetComponent<myController>().grabbedSomething = true;
            }
        }

        if (grabbed) {

            transform.rotation = currHand.rotation;

            if (currHand == MyInputManager.rightHandController) {
                if (MyInputManager.GripValue('r') == 0) {
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
            if(currHand == MyInputManager.leftHandController){
                if (MyInputManager.GripValue('l') == 0) {
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
