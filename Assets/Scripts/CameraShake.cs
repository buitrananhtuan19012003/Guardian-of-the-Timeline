using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource cinemachineImpulseSource;
    void Update()
    {
        cinemachineImpulseSource.GenerateImpulse(Camera.main.transform.forward);
    }
}
