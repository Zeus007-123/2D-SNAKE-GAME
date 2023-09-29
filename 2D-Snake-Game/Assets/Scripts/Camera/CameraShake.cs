using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [Header("Cinemachine Virtual Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Header("Camera Shake Amplitude")]
    [SerializeField] private float amplitude = 15f;
    [Header("Camera Shake Time")]
    [SerializeField] private float shakeTime = 0.2f;

    private CinemachineBasicMultiChannelPerlin noise;

    public static CameraShake Instance { get { return instance; } }
    private static CameraShake instance;
     
    private Coroutine shake; // to store coroutine
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
        // check for running coroutine
        if(shake != null)
            StopCoroutine(shake); // if running, then stop
        // start next coroutine
        shake = StartCoroutine(ShakeCoroutine()); 
    }

    IEnumerator ShakeCoroutine()
    {
        noise.m_AmplitudeGain = amplitude;
        yield return waitTime;
        noise.m_AmplitudeGain = 0;
    }
}
