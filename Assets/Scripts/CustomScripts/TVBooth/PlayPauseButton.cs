using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayPauseButton : MonoBehaviour {
    public VideoPlayer vp;
    private TVButton _tvButton;

    private bool isPlaying = true;
    // Start is called before the first frame update
    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tvButton.selected) {
            //Debug.Log("here");
            if (MyInputManager.TriggerValue(_tvButton.handIndicator) > 0.0f) {
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
        enabled = false;
        yield return new WaitForSeconds(time / 1000);
        enabled = true;
    }
}
