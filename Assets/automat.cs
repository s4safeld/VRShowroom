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
            foreach (Transform child in transform) {
                child.localPosition *= explosionValue;
            }
            exploded = true;
        }
        else {
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
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        disabled = true;
        yield return new WaitForSeconds(time/1000);
        disabled = false;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
