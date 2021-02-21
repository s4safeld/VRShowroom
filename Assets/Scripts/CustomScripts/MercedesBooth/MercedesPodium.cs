using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MercedesPodium : MonoBehaviour
{
    public bool rotate = true;
    public float rotationSpeed = 1f;

    void Update() {
        if (rotate) {
            transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
            GetComponent<TeleportationArea>().enabled = false;
        }
        else
            GetComponent<TeleportationArea>().enabled = true;
    }
}
