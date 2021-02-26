using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCube : MonoBehaviour {
    public Collider[] colliders;
    private bool[] a;
    // Start is called before the first frame update
    void Start()
    {
        a = new bool[colliders.Length];
        for (int i = 0; i < a.Length; i++) {
            a[i] = false;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i] == other) {
                a[i] = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i] == other) {
                a[i] = false;
            }
        }
    }

    public bool IsInside(Collider col) {
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i] == col) {
                return a[i];
            }
        }
        return false;
    }
}
