using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mercedesRemote : MonoBehaviour
{

    private Remote remote;
    public GameObject device;
    public Transform[] transformsToExpand;
    public Vector3[] DestinationVector3s;
    public AudioSource audioSource;
    private ObjectToExpand[] _objectsToExpand;
    private bool expanded = false;
    private Vector2 primary2DAxis;
    private MercedesPodium mercedesPodium;
    private bool disabled;
    
    // Start is called before the first frame update
    private void Start() {
        remote = GetComponent<Remote>();
        mercedesPodium = device.GetComponent<MercedesPodium>();

        _objectsToExpand = new ObjectToExpand[transformsToExpand.Length];
        for (int i = 0; i < _objectsToExpand.Length; i++) {
            _objectsToExpand[i] = new ObjectToExpand(transformsToExpand[i], DestinationVector3s[i]);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (disabled) return;
        if (remote.grabbed &&(MyInputManager.Primary2DAxisClick('l') || MyInputManager.Primary2DAxisClick('r'))) {
            if (remote.grabbed) {
                
                primary2DAxis = MyInputManager.Primary2DAxis(remote.currHand == MyInputManager.leftHandController ? 'l' : 'r');
                
                //explode
                if (primary2DAxis.y > 0 && primary2DAxis.x <= 0) {
                    if (!expanded) {
                        expanded = true;
                        foreach (var ote in _objectsToExpand) {
                            ote.Transform.localPosition = ote.Destination;
                        }
                        audioSource.pitch = 1;
                        audioSource.Play();
                    }
                    else {
                        expanded = false;
                        foreach (var ote in _objectsToExpand) {
                            ote.Transform.localPosition = ote.Origin;
                        }
                        audioSource.pitch = -1;
                        audioSource.Play();
                    }
                    StartCoroutine(WaitForMilliseconds(500));
                }
                //Pause Rotation
                if (primary2DAxis.y > 0 && primary2DAxis.x > 0) {
                    mercedesPodium.rotate = !mercedesPodium.rotate;
                    StartCoroutine(WaitForMilliseconds(500));
                }
                //Rotate left
                if (primary2DAxis.y < 0 && primary2DAxis.x <= 0) {
                    if (mercedesPodium.rotate) {
                        device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed*2, 0f) * Time.deltaTime);
                    }
                    else {
                        device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                    }
                }
                //Rotate right
                if (primary2DAxis.y < 0 && primary2DAxis.x > 0) {
                    if (mercedesPodium.rotate) {
                        device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed / 2, 0f) * Time.deltaTime);
                    }
                    else {
                        device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed, 0f) * Time.deltaTime); 
                    }
                }
            }
        }
    }

    IEnumerator WaitForMilliseconds(float time) {
        disabled = true;
        yield return new WaitForSeconds(time / 1000);
        disabled = false;
    }
}

public class ObjectToExpand {
    public Transform Transform;
    public Vector3 Destination;
    public Vector3 Origin;

    public ObjectToExpand(Transform transform, Vector3 destination) {
        this.Transform = transform;
        this.Destination = destination;
        Origin = transform.localPosition;
    }
}
