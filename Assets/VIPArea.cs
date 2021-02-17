using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VIPArea : MonoBehaviour {
    public TeleportationArea[] tpAreas;
    public string passCode;
    public MeshRenderer[] barriers;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tpArea in tpAreas) {
            foreach (var collider in tpArea.colliders) {
                collider.enabled = false;
            }
        } 
    }

    public void allowAccess(string passCode ,bool input) {
        if (input && passCode.Equals(this.passCode)) {
            foreach (var tpArea in tpAreas) {
                foreach (var col in tpArea.colliders) {
                    col.enabled = true;
                }
            }
            foreach (var barrier in barriers) {
                barrier.enabled = true;
            }
        }
    }
}
