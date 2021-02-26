using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenseCoffee : MonoBehaviour {
    public GameObject cups;
    private Collider[] _cupColliders;
    // Start is called before the first frame update
    private void Start() {
        _cupColliders = cups.transform.GetComponentsInChildren<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        foreach (var col in _cupColliders) {
            if (other == col) {
                other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
