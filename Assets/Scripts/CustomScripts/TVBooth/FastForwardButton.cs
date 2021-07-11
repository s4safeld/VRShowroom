using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FastForwardButton : MonoBehaviour {
    public VideoPlayer _vp;
    private TVButton _tvButton;
    private bool fastForwarding = false;
    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tvButton.selected && !fastForwarding && MyInputManager.TriggerButtonPressed('l')) {
            fastForwarding = true;
            _vp.playbackSpeed = 10;
        }
        if (!_tvButton.selected && fastForwarding) {
            fastForwarding = false;
            _vp.playbackSpeed = 1;
        }
    }
}
