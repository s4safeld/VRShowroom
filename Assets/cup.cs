using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cup : MonoBehaviour {
    public MeshRenderer content;
    

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(Vector3.up, transform.up) > 90) {
            content.enabled = false;
        }
    }
}
