using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBasicMultiChannelPerlin noise;
    public float amplitude;
    public float shakeTime;
    public static CameraShake instance;

    private WaitForSeconds waitTime;
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        waitTime = new(shakeTime);
    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        noise.m_AmplitudeGain = amplitude;
        yield return waitTime;
        noise.m_AmplitudeGain = 0;
    }
}
