using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class TVButton : MonoBehaviour {
    public bool selected = false;
    public float grabbingRange = 0.2f;
    public bool listenForRaycast;
    public char handIndicator = '0';
    
    private Collider _collider;
    private Outline _outline;

    private void Start() {
        _collider = GetComponent<Collider>();
        _outline = GetComponent<Outline>();
    }

    private void Update() {
        if (listenForRaycast)
            handIndicator = MyInputManager.HoveredByRayInteractor(_collider);
        if(listenForRaycast && handIndicator == '0')
            handIndicator = MyInputManager.InRange(transform.position, grabbingRange);
        if(!listenForRaycast)
            handIndicator = MyInputManager.InRange(transform.position, grabbingRange);
        
        if (handIndicator != '0') {
            Select( true);
        }
        else {
            Select( false);
        }
    }

    private void Select(bool input) {
        _outline.enabled = input;
        selected = input;
    }
}
