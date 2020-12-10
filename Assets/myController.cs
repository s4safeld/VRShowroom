using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myController : MonoBehaviour {
    private LineRenderer lr;
    private InputManager im;
    private char hand;
    private Rigidbody currRigidbody;
    private bool triggerPressed = false;
    private Transform oldHand;

    private List<Rigidbody> contactRigidBodies = new List<Rigidbody>();
    
    void Start() {
        lr = GetComponent<LineRenderer>();
        im = FindObjectOfType<InputManager>();

        if (name.Contains("eft")) {
            hand = 'l';
        }

        if (name.Contains("ight")) {
            hand = 'r';
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (im.GripValue(hand)>0.0f) {
            lr.enabled = true;
        }
        else {
            lr.enabled = false;
        }
        
        if (im.TriggerButtonPressed(hand) && !triggerPressed) {
            triggerPressed = true;
            currRigidbody = GetNearestRigidBody();
            Debug.Log(currRigidbody.name);
            foreach (Transform child in transform) {
                if (child.GetComponent<Rigidbody>() != null) {
                    oldHand = child;
                    break;
                }
            }
            oldHand.transform.parent = null;
            oldHand.GetComponent<Collider>().enabled = true;
            oldHand.transform.localPosition = currRigidbody.position;
            oldHand.transform.localRotation = Quaternion.identity;
            currRigidbody.transform.parent = transform;
            currRigidbody.transform.localPosition = Vector3.zero;
            currRigidbody.transform.localRotation = Quaternion.identity;
            currRigidbody.GetComponent<Collider>().enabled = false;

            contactRigidBodies.Remove(currRigidbody);
        }

        if (!im.TriggerButtonPressed(hand) && triggerPressed) {
            triggerPressed = false;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        int i = 0;
        if (!other.gameObject.GetComponent<Rigidbody>()) {
            return;
        }

        contactRigidBodies.Add(other.gameObject.GetComponent<Rigidbody>());
        foreach (Rigidbody crb in contactRigidBodies) {
            Debug.LogWarning("contactRigidbody[" + i + "] : " + crb.name);
            i++;
        }
    }

    void OnTriggerExit(Collider other) {
        int i = 0;
        if (!other.gameObject.GetComponent<Rigidbody>()) {
            return;
        }

        contactRigidBodies.Remove(other.gameObject.GetComponent<Rigidbody>());
        foreach (Rigidbody crb in contactRigidBodies) {
            Debug.LogWarning("contactRigidbody[" + i + "] : " + crb.name);
            i++;
        }
    }

    private Rigidbody GetNearestRigidBody() {
        Rigidbody nearestRigidBody = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;
        foreach (Rigidbody contactBody in contactRigidBodies) {
            distance = (contactBody.gameObject.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance) {
                minDistance = distance;
                nearestRigidBody = contactBody;
            }
        }

        return nearestRigidBody;
    }
}
