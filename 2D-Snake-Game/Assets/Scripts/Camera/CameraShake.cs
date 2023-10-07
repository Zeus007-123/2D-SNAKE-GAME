using System.Collections;
using UnityEngine;
using Cinemachine;

    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float amplitude = 15f;
        [SerializeField] private float shakeTime = 0.2f;

        private CinemachineBasicMultiChannelPerlin noise;

        public static CameraShake Instance { get { return instance; } }
        private static CameraShake instance;

        private Coroutine shakeCoroutine;
        private WaitForSeconds waitTime;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            instance = this;
        }
        void Start()
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            waitTime = new(shakeTime);

        }
        public void ShakeCamera()
        {
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(ShakeCoroutine());
        }
        IEnumerator ShakeCoroutine()
        {
            noise.m_AmplitudeGain = amplitude;
            yield return waitTime;
            noise.m_AmplitudeGain = 0;
        }
    }

