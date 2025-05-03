using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] internal float _shakeTime=0;
    [SerializeField] internal float _shakeIntensity=0;



    internal void ShakeCamera(float intensity, float time)
    {
        _shakeTime=time;
        _shakeIntensity = time;
        CinemachineBasicMultiChannelPerlin perlin=_camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.AmplitudeGain = intensity;
        perlin.FrequencyGain = time;
    }
}
