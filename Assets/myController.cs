using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myController : MonoBehaviour {
    public bool grabbedSomething = false;
    private MyInputManager mim;
    public char leftOrRight;
    private Vector2 primary2DAxis;
    void Start() {
        mim = FindObjectOfType<MyInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mim.Primary2DAxisClick(leftOrRight)) {
            primary2DAxis = mim.Primary2DAxis(leftOrRight);
            if (primary2DAxis.y < 0) {
                foreach (GameObject controller in ControllerStorage.storage) {
                    Debug.Log(controller.name + " is in storage",this);
                }
            }
        }
    }
}
