using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random = System.Random;

//using Random = System.Random;

//using Random = System.Random;

public class cup : MonoBehaviour {
    public MeshRenderer content;
    public GameObject decalPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(Vector3.up, transform.up) > 90 && content.enabled) {
            content.enabled = false;
            GetComponent<AudioSource>().Play();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit,Mathf.Infinity)) {
                CreateDecal(hit);
            }
        }
    }

    void CreateDecal(RaycastHit hit) {
        var decal = Instantiate(decalPrefab);
        
        decal.transform.forward = hit.normal * -1f;
        decal.transform.position = hit.point;
        decal.transform.Translate(Vector3.forward * -0.00001f);
        decal.transform.Rotate(Vector3.forward, Random.Range(-360,360));
    }
}
