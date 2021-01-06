using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHolster : MonoBehaviour {
    public Transform mainCam;
    public float offset;

    // Update is called once per frame
    void FixedUpdate() {
        if(mainCam.hasChanged)
            transform.position = mainCam.position - new Vector3(0,offset,0);
    }
}
