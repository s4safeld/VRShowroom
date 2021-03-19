using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateObject : MonoBehaviour {
    private bool _rotated = false;
    private bool _selected = false;
    private bool _triggerPressed = false;
    private Collider _col;
    private Animator _animator;
    private Outline _outline;

    private char handIndicator;

    public string openAnim;
    public string closeAnim;
    // Start is called before the first frame update
    void Start() {
        _col = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update() {
        handIndicator = MyInputManager.HoveredByRayInteractor(_col);
        
        if (handIndicator != '0') {
            _outline.enabled = true;
            _selected = true;
            if (MyInputManager.TriggerValue(handIndicator) <= 0) {
                _triggerPressed = false;
            }

            Debug.Log(!_triggerPressed+","+ (MyInputManager.TriggerValue(handIndicator)>0)+","+ _selected);
            if (!_triggerPressed
                && (MyInputManager.TriggerValue(handIndicator) > 0)
                && _selected) {
                _triggerPressed = true;

                if (!_rotated) {
                    _animator.Play(closeAnim);
                    _rotated = true;
                }

                if (_rotated) {
                    _animator.Play(openAnim);
                    _rotated = false;
                }
            }
        }
        else {
            _outline.enabled = false;
            _selected = false;
        }
    }
}
