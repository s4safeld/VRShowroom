using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateObject : MonoBehaviour {
    private bool _rotated = false;
    private bool _selected = false;
    private bool _gripPressed = false;
    private Collider _col;
    private MyInputManager _mim;
    private Animator _animator;

    public TextMeshPro[] descriptionTexts;
    public string openAnim;
    public string closeAnim;
    // Start is called before the first frame update
    void Start() {
        _col = GetComponent<Collider>();
        _mim = FindObjectOfType<MyInputManager>();
        _animator = GetComponent<Animator>();

        foreach (var text in descriptionTexts) {
            text.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_mim.HoveredByRayInteractor(_col)) {
            _selected = true;
            foreach (var text in descriptionTexts) {
                text.enabled = true;
            }
        }
        else {
            _selected = false;
            foreach (var text in descriptionTexts) {
                text.enabled = false;
            }
        }
        
        if (_mim.TriggerValue('l') <= 0 && _mim.TriggerValue('r') <= 0) {
            _gripPressed = false;
        }

        //Debug.Log(!_gripPressed+","+ (_mim.TriggerValue('l') > 0 || _mim.TriggerValue('r') > 0)+","+ _selected);
        if (!_gripPressed 
            &&(_mim.TriggerValue('l') > 0 || _mim.TriggerValue('r') > 0)
            && _selected)
        {
            _gripPressed = true;
            
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
}
