using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class Hover : MonoBehaviour {
    static bool hover = false;
    public VideoPlayer vp;

    // Update is called once per frame
    void FixedUpdate() {
        if (hover && transform.localPosition.z < 0.15f)
            this.transform.localPosition += Vector3.forward *Time.time / 1000;

        if (!hover && transform.localPosition.z > 0.09f)
            this.transform.localPosition -= Vector3.forward * Time.time / 1000;
    }

    public void Select(bool input) {
        hover = input;
        foreach (Light light in GetComponentsInChildren<Light>()) {
            light.enabled = input;
        }
    }

    public void TogglePlayback() {
        if (vp.isPaused) {
            Debug.Log("VP is paused",this);
            vp.Play();
        }
        else {
            vp.Pause();
        }
    }
}
