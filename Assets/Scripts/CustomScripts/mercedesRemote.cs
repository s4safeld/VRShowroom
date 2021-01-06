using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mercedesRemote : MonoBehaviour
{

    private Remote remote;
    private MyInputManager mim;
    public GameObject device;
    private Vector2 primary2DAxis;
    private MercedesPodium mercedesPodium;
    private bool disabled;
    
    // Start is called before the first frame update
    private void Start() {
        remote = GetComponent<Remote>();
        mim = FindObjectOfType<MyInputManager>();
        mercedesPodium = device.GetComponent<MercedesPodium>();
    }

    // Update is called once per frame
    private void Update() {
        if (disabled) return;
        if (mim.Primary2DAxisClick('l')) {
            if (remote.grabbed && remote.currHand == mim.leftHandController) {
                primary2DAxis = mim.Primary2DAxis('l');
                if (primary2DAxis.y >= 0) {
                    mercedesPodium.rotate = !mercedesPodium.rotate;
                    StartCoroutine(WaitForMilliseconds(100));
                }
                if (primary2DAxis.y < 0 && primary2DAxis.x <= 0) {
                    if (mercedesPodium.rotate) {
                        device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed*2, 0f) * Time.deltaTime);
                    }
                    else {
                        device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                    }
                }
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

        if (mim.Primary2DAxisClick('r')) {
            if (remote.grabbed && remote.currHand == mim.rightHandController) {
                primary2DAxis = mim.Primary2DAxis('r');
                if (primary2DAxis.y >= 0) {
                    mercedesPodium.rotate = !mercedesPodium.rotate;
                    StartCoroutine(WaitForMilliseconds(100));
                }
                if (primary2DAxis.y < 0 && primary2DAxis.x <= 0) {
                    mercedesPodium.rotate = false;
                    device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                    mercedesPodium.rotate = true;
                }
                if (primary2DAxis.y < 0 && primary2DAxis.x > 0) {
                    mercedesPodium.rotate = false;
                    device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                    mercedesPodium.rotate = true;
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
