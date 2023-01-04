using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public int _frameRate = 60;

    void Start () {
#if UNITY_STANDALONE
		QualitySettings.vSyncCount = 0;
#endif
    }

    void Update () {
#if UNITY_STANDALONE
		if (_frameRate != Application.targetFrameRate)
			Application.targetFrameRate = _frameRate;
#endif
    }
}
