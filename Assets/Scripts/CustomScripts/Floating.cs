using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
    private float start;
    // Start is called before the first frame update
    void Start() {
        start = Random.Range(-5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        var transformLocalPosition = transform.localPosition;
        transformLocalPosition.z += (0.001f * Mathf.Cos(Time.time+start));
        transform.localPosition = transformLocalPosition;
        transform.Rotate(Vector3.up,Mathf.Cos((50f * Time.deltaTime)));
    }
}
