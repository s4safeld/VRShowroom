using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {
    private Material mat;
    public float speed;
    private bool activated;
    private Vector2 offset;
    
    void Start() {
        mat = GetComponent<MeshRenderer>().materials[1];
    }
    
    void FixedUpdate() {
        activated = MyInputManager.InRange(transform.position, 2) != '0';
        offset = mat.mainTextureOffset;

        if (activated && offset.y >= 0) {
            offset = Vector2.Lerp(offset, new Vector2(0, 0), speed);
        }
        
        if (!activated && offset.y <= 0.5f) {
            offset = Vector2.Lerp(offset, new Vector2(0, 0.5f), speed);
        }
        
        mat.mainTextureOffset = offset;
    }
}
