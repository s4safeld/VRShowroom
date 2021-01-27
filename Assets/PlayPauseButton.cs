﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayPauseButton : MonoBehaviour {
    private MyInputManager mim;
    public VideoPlayer vp;
    private TVButton _tvButton;

    private bool isPlaying = true;
    // Start is called before the first frame update
    void Start() {
        mim = FindObjectOfType<MyInputManager>();
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tvButton.selected) {
            //Debug.Log("here");
            if (mim.TriggerValue('l') > 0.0f || mim.TriggerValue('r') > 0.0f) {
                Debug.Log("here",this);
                if (isPlaying) {
                    vp.Pause();
                    isPlaying = false;
                }
                else {
                    vp.Play();
                    isPlaying = true;
                }
                StartCoroutine(WaitForMilliseconds(500));
            }
        }
    }

    IEnumerator WaitForMilliseconds(float time) {
        this.enabled = false;
        yield return new WaitForSeconds(time / 1000);
        enabled = true;
    }
}
