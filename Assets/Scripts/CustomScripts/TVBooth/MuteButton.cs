using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MuteButton : MonoBehaviour {
    public VideoPlayer _vp;
    private TVButton _tvButton;
    private bool muteToggle = false;
    private bool muted = false;

    public bool listenForTrigger;
    
    // Start is called before the first frame update
    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tvButton.selected && !muteToggle) {
            muteToggle = true;
            _vp.SetDirectAudioMute(0,muted = !muted);
        }
        if(!_tvButton.selected && muteToggle){
            muteToggle = false;
        }
    }

    IEnumerator WaitForMilliseconds(float time) {
        enabled = false;
        yield return new WaitForSeconds(time / 1000);
        enabled = true;
    }
}
