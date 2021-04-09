using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class shiftscreen : MonoBehaviour {
    private Collider col;
    private Vector3 leftHandControllerPosition;
    private Vector3 rightHandControllerPosition;

    public Spline spline;

    public Transform[] tokens;

    // Start is called before the first frame update
    void Start() {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        leftHandControllerPosition = MyInputManager.leftHandController.position;
        rightHandControllerPosition = MyInputManager.rightHandController.position;

        if (MyInputManager.GripValue('r') > 0 && col.bounds.Contains(rightHandControllerPosition)) {
            transform.position = spline.WhereOnSpline(rightHandControllerPosition);
        }else if (MyInputManager.GripValue('l') > 0 && col.bounds.Contains(leftHandControllerPosition)){
            transform.position = spline.WhereOnSpline(leftHandControllerPosition);
        }

        for (int i = 0; i < transform.childCount; i++) {
            if(i == NearestToken())
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    int NearestToken() {
        float distance = float.MaxValue;
        int nearestToken = 0;
        for (int i = 0; i < tokens.Length; i++) {
            if (Vector3.Distance(transform.position, tokens[i].position) < distance) {
                distance = Vector3.Distance(transform.position, tokens[i].position);
                nearestToken = i;
            }
        }
        return nearestToken;
    }
}
