using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class TVButton : MonoBehaviour {
    public bool selected = false;
    public AnimationClip SelectAnimation;
    public AnimationClip DeselectAnimation;
    private Collider _collider;

    private MyInputManager mim;

    private void Start() {
        mim = FindObjectOfType<MyInputManager>();
        _collider = GetComponent<Collider>();
    }

    private void Update() {
        if (mim.InRange(transform.position, 'a') || mim.HoveredByRayInteractor(_collider)) {
            Select(selected = true);
        }
        else {
            Select(selected = false);
        }
    }

    public void Select(bool input) {
        foreach (Light light in GetComponentsInChildren<Light>()) {
            light.enabled = input;
        }
        if (input) {
            //GetComponent<Animation>().Play("ButtonAnimationSelect");
            GetComponent<Animator>().Play(SelectAnimation.name);
        }
        else {
           // GetComponent<Animation>().Play("ButtonAnimationDeselect");
           GetComponent<Animator>().Play(DeselectAnimation.name);
        }
    }
}
