using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolvoBooth : MonoBehaviour
{
    public bool rotate = true;
    public float rotationSpeed = 1f;
    public bool paused = false;

    void Update() {
        if(!paused)
            transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
    }
}
