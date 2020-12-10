using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class InputDebugger : MonoBehaviour {
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start() {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputManager.Primary2DAxisClick('l')) {
            Debug.Log("click pressed");
        }

    }
}
