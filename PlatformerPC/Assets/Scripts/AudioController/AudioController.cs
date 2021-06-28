using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField] private AudioSource _effectsAudioSource;
    [SerializeField] private AudioSource _UIAudioSource;
    [SerializeField] private AudioSource _playerBulletsSource;
    [SerializeField] private AudioSource _enemyBulletsSource;

    [SerializeField] private AudioSource _playerStepsSource;

    [Header("Shoot")]
    [SerializeField] private AudioClip _commonShotClip;
    [SerializeField] private AudioClip _strengthenedShotClip;

    [Header("Reload")]
    [SerializeField] private AudioClip _reloadQuickClip;
    [SerializeField] private AudioClip _reloadShortClip;
    [SerializeField] private AudioClip _reloadMediumClip;
    [SerializeField] private AudioClip _reloadLongClip;

    [Header("Purchase")]
    [SerializeField] private AudioClip _successfulPurchase;
    [SerializeField] private AudioClip _unsuccessfulPurchase;

    [Header("Button")]
    [SerializeField] private AudioClip _onButtonClick;

    [Header("BulletHit")]
    [SerializeField] private AudioClip _commonBulletBodyHit;
    [SerializeField] private AudioClip _commonBulletHeadHit;
    [SerializeField] private AudioClip _commonBulletShieldHit;
    [SerializeField] private AudioClip _strengthenedBulletHit;

    [Header("Win lose screen")]
    [SerializeField] private AudioClip _winScreenClip;
    [SerializeField] private AudioClip _loseScreenClip;

    [Header("Steps")]
    [SerializeField] private AudioClip[] _step;

    [Header("Other")]
    [SerializeField] private AudioClip _openChest;
    [SerializeField] private AudioClip _shopEntrance;
    [SerializeField] private AudioClip _upgradeSound;

    [SerializeField] private AudioClip _healing;
    [SerializeField] private AudioClip _chargeShield;
    [SerializeField] private AudioClip _checkPointSound;

    public static AudioController Instance { get; private set; }

    public bool IsRunning;

    public float CurrentPlayerMoveSpeed;
    private float _playerMaxMoveSpeed;
    private float _playerStepTime;



    void Awake()
    {
        Instance = this;
        PlayEffectsSound();
    }


    void Start()
    {
        _playerMaxMoveSpeed = GlobalDefVals.PLAYER_MAX_MOVE_SPEED;
    }


    void Update()
    {
        _playerStepTime -= Time.deltaTime;
        PlayPlayerStepSound();
    }


    public void PlayButtonSound()
    {
        _UIAudioSource.pitch = Random.Range(0.97f, 1.03f);
        _UIAudioSource.PlayOneShot(_onButtonClick);
    }

    #region Player shot sounds

    public void PlayPlayerCommonShotSound()
    {
        _playerBulletsSource.pitch = Random.Range(0.91f, 1.05f);
        _playerBulletsSource.PlayOneShot(_commonShotClip);
    }


    public void PlayPlayerStrengthenedShotSound()
    {
        _playerBulletsSource.pitch = Random.Range(0.9f, 1.18f);
        _playerBulletsSource.PlayOneShot(_strengthenedShotClip);
    }


    public void PlayPlayerCommonBulletBodyHit()
    {
        _playerBulletsSource.pitch = Random.Range(0.95f, 1.05f);
        _playerBulletsSource.PlayOneShot(_commonBulletBodyHit);
    }


    public void PlayPlayerCommonBulletHeadHit()
    {
        _playerBulletsSource.pitch = Random.Range(0.95f, 1.05f);
        _playerBulletsSource.PlayOneShot(_commonBulletHeadHit);
    }


    public void PlayPlayerStrengthenedBulletHit()
    {
        _playerBulletsSource.pitch = Random.Range(0.95f, 1.05f);
        _playerBulletsSource.PlayOneShot(_strengthenedBulletHit, 0.95f);
    }

    #endregion

    #region EnemyShot Sounds

    public void PlayEnemyCommonShotSound()
    {
        _enemyBulletsSource.pitch = Random.Range(0.91f, 1.05f);
        _enemyBulletsSource.PlayOneShot(_commonShotClip);
    }


    public void PlayEnemyCommonBulletShieldHit()
    {
        _enemyBulletsSource.pitch = Random.Range(0.95f, 1.05f);
        _enemyBulletsSource.PlayOneShot(_commonBulletShieldHit);
    }


    public void PlayEnemyCommonBulletBodyHit()
    {
        _enemyBulletsSource.pitch = Random.Range(0.95f, 1.05f);
        _enemyBulletsSource.PlayOneShot(_commonBulletBodyHit);
    }

    #endregion 

    public void PlayPlayerStepSound()
    {
        if (_playerStepTime <= 0 && IsRunning)
        {
            _playerStepsSource.volume = (CurrentPlayerMoveSpeed / _playerMaxMoveSpeed);
            _playerStepsSource.pitch = Random.Range(0.97f, 1.03f);
            _playerStepsSource.PlayOneShot(_step[Random.Range(0, 2)]);
            _playerStepTime = 0.25f;
        }
        else if (!IsRunning)
        {
            _playerStepTime = 0.1f;
        }
    }


    public void PlayHealSound()
    {
        _effectsAudioSource.PlayOneShot(_healing);
    }


    public void PlayChargeShieldSound()
    {
        _effectsAudioSource.PlayOneShot(_chargeShield);
    }


    public void PlayEffectsSound()
    {
        _effectsAudioSource.UnPause();
    }


    public void PlayCheckPointSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_checkPointSound);
    }


    public void PauseEffectsSound()
    {
        _effectsAudioSource.Pause();
    }


    public void PlayLoseScreenSound()
    {
        _UIAudioSource.PlayOneShot(_loseScreenClip);
    }


    public void PlayWinScreenSound()
    {
        _UIAudioSource.PlayOneShot(_winScreenClip);
    }


    public void PlayOpenChestSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_openChest);
    }


    public void PlayUpgradeSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_upgradeSound);
    }


    public void PlayShopEntranceSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_shopEntrance);
    }


    public void PlaySuccessfulPurchaseSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_successfulPurchase);
    }


    public void PlayUnsuccessfulPurchaseSound()
    {
        _effectsAudioSource.pitch = 1f;
        _effectsAudioSource.PlayOneShot(_unsuccessfulPurchase);
    }


    public void PlayEnemyReloadSound()
    {
        _effectsAudioSource.PlayOneShot(_reloadLongClip, 0.5f);
    }


    public void PlayPlayerReloadSound()
    {
        float reloadTime = GlobalDefVals.PLAYER_RELOAD_TIME - PlayerUpgrades.ReloadTimeDecrease.Value;

        if (reloadTime >= 2.2f)
        {
            _effectsAudioSource.PlayOneShot(_reloadLongClip);
        }
        else if (reloadTime >= 1.89f)
        {
            _effectsAudioSource.PlayOneShot(_reloadMediumClip);
        }
        else if (reloadTime >= 1.65f)
        {
            _effectsAudioSource.PlayOneShot(_reloadShortClip);
        }
        else if (reloadTime >= 1.4f)
        {
            _effectsAudioSource.PlayOneShot(_reloadQuickClip);
        }
    }
}
