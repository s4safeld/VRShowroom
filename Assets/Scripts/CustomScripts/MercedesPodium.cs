using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercedesPodium : MonoBehaviour
{
    public bool rotate = true;
    public float rotationSpeed = 1f;

    void Update() {
        if(rotate)
            transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
    }
}
