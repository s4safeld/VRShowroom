using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFlipper : MonoBehaviour {
    public bool forward;
    public Renderer screen;
    public Texture[] textures;
    private TVButton _tvButton;
    private static int i=0;
    private bool pageFlipped = false;
    // Start is called before the first frame update
    void Start() {
        _tvButton = GetComponent<TVButton>();
    }

    // Update is called once per frame
    void Update() {
        if (_tvButton.selected && !pageFlipped) {
            if (forward) {
                if (i > textures.Length)
                    i = -1;
                screen.material.mainTexture = textures[++i];
            }
            else {
                if (i < 0)
                    i = textures.Length - 1;
                screen.material.mainTexture = textures[--i];
            }
            pageFlipped = true;
        }

        if (!_tvButton.selected) {
            pageFlipped = false;
        }
    }
}
