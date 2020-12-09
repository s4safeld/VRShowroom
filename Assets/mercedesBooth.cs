using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mercedesBooth : MonoBehaviour
{
    public bool rotate = true;
    public float rotationSpeed = 1f;
    void Update()
    {
        transform.Rotate(new Vector3(0f, -rotationSpeed, 0f) * Time.deltaTime);
    }
}
