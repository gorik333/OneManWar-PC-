using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachine;

    public static CinemachineShake Instance { get; private set; }

    private float _shakerTimer;
    private float _totalShakerTimer;
    private float _startingIntensity;


    private float _timerBetweenShots;


    void Awake()
    {
        Instance = this;
        _cinemachine = GetComponent<CinemachineVirtualCamera>();
    }


    public void ShakeCamera(float intensity, float time)
    {
        if (_timerBetweenShots >= GlobalDefVals.PLAYER_TIME_BETWEEN_SHOTS / (PlayerUpgrades.AdditionalFireRate.Value + GlobalDefVals.FIRE_RATE))
        {
            CinemachineBasicMultiChannelPerlin channelPerlin = _cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            channelPerlin.m_AmplitudeGain = intensity;
            _startingIntensity = intensity;
            _totalShakerTimer = time;
            _shakerTimer = time;
            _timerBetweenShots = 0;
        }
    }


    void Update()
    {
        if (_shakerTimer > 0)
        {
            _shakerTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin channelPerlin = _cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            channelPerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, _shakerTimer / _totalShakerTimer);
        }

        _timerBetweenShots += Time.deltaTime;
    }
}
