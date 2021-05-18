using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mercedesRemote : MonoBehaviour
{

    private Tool remote;
    public GameObject device;
    public Transform[] transformsToExpand;
    public Vector3[] DestinationVector3s;
    public AudioSource audioSource;
    private ObjectToExpand[] _objectsToExpand;
    private bool expanded = false;
    private Vector2 primary2DAxis;
    private MercedesPodium mercedesPodium;
    private bool rotationPaused = false;
    private bool disabled;
    
    // Start is called before the first frame update
    private void Start() {
        remote = GetComponent<Tool>();
        mercedesPodium = device.GetComponent<MercedesPodium>();

        _objectsToExpand = new ObjectToExpand[transformsToExpand.Length];
        for (int i = 0; i < _objectsToExpand.Length; i++) {
            _objectsToExpand[i] = new ObjectToExpand(transformsToExpand[i], DestinationVector3s[i]);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (disabled) return;

        #if UNITY_ANDROID
        bool fire = MyInputManager.TriggerValue('r') > 0.5f;
        #else
        bool fire = MyInputManager.Primary2DAxisClick('r');
        #endif
        
        if (remote.grabbed && fire) {
            primary2DAxis = MyInputManager.Primary2DAxis(remote.currHandIndicator);
                
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
                rotationPaused = !rotationPaused;
                StartCoroutine(WaitForMilliseconds(500));
            }
            //Rotate left
            if (primary2DAxis.y < 0 && primary2DAxis.x <= 0) {
                if (!rotationPaused)
                    mercedesPodium.rotate = false;

                device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                
                if (!rotationPaused)
                    mercedesPodium.rotate = true;
            }
            //Rotate right
            if (primary2DAxis.y < 0 && primary2DAxis.x > 0) {
                if (!rotationPaused)
                    mercedesPodium.rotate = false;

                device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);

                if (!rotationPaused)
                    mercedesPodium.rotate = true;
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
