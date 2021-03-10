using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBase : MonoBehaviour {
    public Transform target;
    private Transform _transform;

    private float angle = 0;
    // Start is called before the first frame update
    void Start() {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(new Vector3(target.localPosition.x, 0 ,target.localPosition.z));
    }
}
