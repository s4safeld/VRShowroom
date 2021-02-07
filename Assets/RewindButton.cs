using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RewindButton : MonoBehaviour
{
    public VideoPlayer _vp;
    private TVButton _tvButton;
    private bool rewinding = false;

    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
