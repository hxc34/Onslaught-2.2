using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameplayCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera overheadCamera;
    public CinemachineFreeLook pivotCamera;
    public GameObject pivotObject;

    bool overhead = false;
    float pivotY;

    // Start is called before the first frame update
    void Start()
    {
        pivotY = pivotObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overhead) {
            float yClamp = pivotCamera.m_YAxis.Value;
            if (yClamp <= 0) yClamp = 0.001f;
            if (yClamp > 0.5f) yClamp = 0.5f;
            pivotObject.transform.position = new Vector3(pivotObject.transform.position.x, pivotY + (40 * yClamp), pivotObject.transform.position.z);
        }
    }

    public void ToggleOverhead() {
        overhead = !overhead;
        overheadCamera.gameObject.SetActive(overhead);
        pivotCamera.gameObject.SetActive(!overhead);
    }
}
