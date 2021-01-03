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
    
    // Start is called before the first frame update
    private void Start() {
        remote = GetComponent<Remote>();
        mim = FindObjectOfType<MyInputManager>();
        mercedesPodium = device.GetComponent<MercedesPodium>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mim.Primary2DAxisClick('l')) {
            if (remote.grabbed && remote.currHand == mim.leftHandController) {
                primary2DAxis = mim.Primary2DAxis('l');
                if (primary2DAxis.y >= 0)
                    mercedesPodium.rotate = !mercedesPodium.rotate;
                if(primary2DAxis.y < 0 && primary2DAxis.x<=0)
                    device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                if (primary2DAxis.y < 0 && primary2DAxis.x > 0)
                    device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
            }
        }

        if (mim.Primary2DAxisClick('r')) {
            if (remote.grabbed && remote.currHand == mim.rightHandController) {
                primary2DAxis = mim.Primary2DAxis('r');
                if (primary2DAxis.y >= 0) 
                    mercedesPodium.rotate = !mercedesPodium.rotate;
                if (primary2DAxis.y < 0 && primary2DAxis.x <= 0)
                    device.transform.Rotate(new Vector3(0f, -mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
                if (primary2DAxis.y < 0 && primary2DAxis.x > 0)
                    device.transform.Rotate(new Vector3(0f, mercedesPodium.rotationSpeed, 0f) * Time.deltaTime);
            } 
        }
    }
}
