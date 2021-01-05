using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automat : MonoBehaviour {

    public GameObject remote;
    public float explosionValue;
    public bool selected;
    private bool playing = false;
    private bool exploded = false;
    private bool disabled;

    public void explode() {
        if (disabled) return;
        if (!exploded) {
            //transform.position += new Vector3(0,1,-1);
            foreach (Transform child in transform) {
                child.localPosition *= explosionValue;
            }
            exploded = true;
        }
        else {
            //transform.position -= new Vector3(0, 1, -1);
            foreach (Transform child in transform) {
                child.localPosition /= explosionValue;
            }
            exploded = false;
        }
        StartCoroutine(WaitForMilliseconds(100));
    }

    private void Update() {
        if (selected && !playing) {
            GetComponent<Animator>().StartPlayback();
            playing = true;
        }

        
        if (!selected && playing) {
            GetComponent<Animator>().StopPlayback();
            playing = false;
        }

    }

    IEnumerator WaitForMilliseconds(float time) {
        disabled = true;
        yield return new WaitForSeconds(time/1000);
        disabled = false;
    }
}
