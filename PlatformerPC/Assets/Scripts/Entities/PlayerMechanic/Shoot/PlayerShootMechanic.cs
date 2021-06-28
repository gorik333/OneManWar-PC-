using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShootMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _reloadPanelAnimation;
    [SerializeField] private GameObject _commonBullet;
    [SerializeField] private GameObject _strengthenedBullet;
    [SerializeField] private Transform _playerBody;

    [SerializeField] private Transform _firePoint;

    [SerializeField] private Text _currentMagazineCapacityText;
    [SerializeField] private Text _totalAmmoText;

    private PlayerHealthController _playerHealthController;
    private PlayerAnimationController _animationController;
    private PlayerSwitchAmmoType _switchAmmoType;

    private float _commonOutcomeDamage;
    private float _strengthenedOutcomeDamage;
    private float _fireRate;
    private float _reloadTime;

    private float _maxBulletSpreadAngle;
    private float _timeBetweenShoots;
    private float _currentTimeBetweenShoots = 0f;

    private float _strengthenedBulletSpeed;
    private float _commonBulletSpeed;

    private int _magazineCapacity;

    private int _commonTotalAmmoAmount;
    private int _currentCommonMagazineCapacity;

    private int _totalStrengthenedAmmoAmount;
    private int _currentStrengthenedMagazineCapacity;

    private bool _isReloading;


    void Awake()
    {
        _switchAmmoType = GetComponent<PlayerSwitchAmmoType>();
        _playerHealthController = GetComponent<PlayerHealthController>();
        _animationController = GetComponent<PlayerAnimationController>();

        DefaultValues();
        UpdateValuesOnScreen();
    }


    void Update()
    {
        _currentTimeBetweenShoots += Time.deltaTime;
    }


    void DefaultValues()
    {
        _reloadPanelAnimation.SetActive(false);

        _timeBetweenShoots = GlobalDefVals.PLAYER_TIME_BETWEEN_SHOTS;
        _commonBulletSpeed = GlobalDefVals.COMMON_BULLET_SPEED;
        _strengthenedBulletSpeed = GlobalDefVals.STRENGTHENED_BULLET_SPEED;
        _reloadTime = GlobalDefVals.PLAYER_RELOAD_TIME - PlayerUpgrades.ReloadTimeDecrease.Value;
        _commonOutcomeDamage = GlobalDefVals.PLAYER_DAMAGE + PlayerUpgrades.AdditionalDamage.Value;

        _strengthenedOutcomeDamage = GlobalDefVals.PLAYER_DAMAGE - (GlobalDefVals.PLAYER_DAMAGE / 100 * 30) + 
            PlayerUpgrades.AdditionalDamage.Value - (PlayerUpgrades.AdditionalDamage.Value / 100 * 30);

        _totalStrengthenedAmmoAmount = GlobalDefVals.TOTAL_STRENGTHENED_AMMO;
        _commonTotalAmmoAmount = GlobalDefVals.TOTAL_COMMON_AMMO;

        _magazineCapacity = GlobalDefVals.MAGAZINE_CAPACITY + PlayerUpgrades.AdditionalMagazineCapacity.Value;
        _currentCommonMagazineCapacity = _magazineCapacity;
        _currentStrengthenedMagazineCapacity = _magazineCapacity;

        _maxBulletSpreadAngle = GlobalDefVals.PLAYER_MAX_BULLET_SPREAD_ANGLE;
        _fireRate = GlobalDefVals.FIRE_RATE + PlayerUpgrades.AdditionalFireRate.Value;
    }


    public void Shoot()
    {
        if (_currentTimeBetweenShoots >= (_timeBetweenShoots / _fireRate) && _playerHealthController.IsAlive && !_isReloading &&
            !_switchAmmoType.IsSwitching)
        {
            Quaternion spread = Quaternion.Euler(_playerBody.rotation.eulerAngles + new Vector3(0f, 0f, Random.Range(-_maxBulletSpreadAngle, _maxBulletSpreadAngle)));

            if (!_switchAmmoType.IsSwitchedTypeOfBullet && _currentCommonMagazineCapacity > 0)
            {
                SpawnCommonBullet(spread);
                _animationController.StartCommonShootAnim();
                _currentCommonMagazineCapacity--;
                UpdateValuesOnScreen();

                CinemachineShake.Instance.ShakeCamera(GlobalDefVals.SHAKE_INTENSITY, GlobalDefVals.SHAKE_DURATION);
                AudioController.Instance.PlayPlayerCommonShotSound();

                if (_currentCommonMagazineCapacity <= 0)
                {
                    Reload();
                }
            }
            else if (_switchAmmoType.IsSwitchedTypeOfBullet && _currentStrengthenedMagazineCapacity > 0)
            {
                SpawnStrengthenedBullet(spread);
                _animationController.StartStrengthenedShootAnim();
                _currentStrengthenedMagazineCapacity--;
                UpdateValuesOnScreen();

                CinemachineShake.Instance.ShakeCamera(GlobalDefVals.SHAKE_INTENSITY, GlobalDefVals.SHAKE_DURATION);
                AudioController.Instance.PlayPlayerStrengthenedShotSound();

                if (_currentStrengthenedMagazineCapacity <= 0)
                {
                    Reload();
                }
            }
            _currentTimeBetweenShoots = 0f;
        }
    }


    private void SpawnCommonBullet(Quaternion spread)
    {
        GameObject currentBullet = Instantiate(_commonBullet, _firePoint.position, spread); // ALL WORKS, REMAINS ONLY SET 

        if (_playerBody.rotation.y == 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * _commonBulletSpeed);
        }
        else if (_playerBody.rotation.y != 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * _commonBulletSpeed);
        }

        var currentBulletDamage = currentBullet.GetComponent<PlayerCommonBulletMechanic>();
        currentBulletDamage.Damage = _commonOutcomeDamage;
    }


    private void SpawnStrengthenedBullet(Quaternion spread)
    {
        GameObject currentBullet = Instantiate(_strengthenedBullet, _firePoint.position, spread); // ALL WORKS, REMAINS ONLY SET 

        if (_playerBody.rotation.y == 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * _strengthenedBulletSpeed);
        }
        else if (_playerBody.rotation.y != 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * _strengthenedBulletSpeed);
        }

        var currentBulletProperties = currentBullet.GetComponent<PlayerStrengthenedBulletMechanic>();
        currentBulletProperties.Damage = _strengthenedOutcomeDamage;

        if (_playerBody.rotation.y == 0)
        {
            currentBulletProperties.IsRightDirection = true;
        }
        else
        {
            currentBulletProperties.IsRightDirection = false;
        }
    }


    public void SwitchAmmoType()
    {
        if (!_switchAmmoType.IsSwitching && !_isReloading)
        {
            _switchAmmoType.StartChangeTypeOfBullet(_reloadPanelAnimation);
        }
    }


    public void Reload()
    {
        if (!_switchAmmoType.IsSwitching && !_isReloading)
        {
            if (!_switchAmmoType.IsSwitchedTypeOfBullet && _commonTotalAmmoAmount > 0 && _currentCommonMagazineCapacity != _magazineCapacity)
            {
                AudioController.Instance.PlayPlayerReloadSound();
                CommonAmmoReload();
            }
            else if (_switchAmmoType.IsSwitchedTypeOfBullet && _totalStrengthenedAmmoAmount > 0 && _currentStrengthenedMagazineCapacity != _magazineCapacity)
            {
                AudioController.Instance.PlayPlayerReloadSound();
                StrengthenedAmmoReload();
            }
        }
    }


    public void TurnOffReloadingPanel()
    {
        _reloadPanelAnimation.SetActive(false);
    }


    public void AddCommonAmmo(int count)
    {
        _commonTotalAmmoAmount += count;
        UpdateValuesOnScreen();
    }


    public void AddStrengthenedAmmo(int count)
    {
        _totalStrengthenedAmmoAmount += count;
        UpdateValuesOnScreen();
    }


    public void UpdateValuesOnScreen()
    {
        if (!_switchAmmoType.IsSwitchedTypeOfBullet)
        {
            _currentMagazineCapacityText.text = _currentCommonMagazineCapacity.ToString() + "/";
            _totalAmmoText.text = _commonTotalAmmoAmount.ToString();
        }
        else
        {
            _currentMagazineCapacityText.text = _currentStrengthenedMagazineCapacity.ToString() + "/";
            _totalAmmoText.text = _totalStrengthenedAmmoAmount.ToString();
        }
    }


    private void CommonAmmoReload()
    {
        StartReloading();
        StartCoroutine(ReloadDelay());
        if (_commonTotalAmmoAmount >= (_magazineCapacity - _currentCommonMagazineCapacity))
        {
            _commonTotalAmmoAmount -= _magazineCapacity - _currentCommonMagazineCapacity;
            _currentCommonMagazineCapacity = _magazineCapacity;
        }
        else
        {
            _currentCommonMagazineCapacity += _commonTotalAmmoAmount;
            _commonTotalAmmoAmount = 0;
        }
    }


    private void StrengthenedAmmoReload()
    {
        StartReloading();
        StartCoroutine(ReloadDelay());
        if (_totalStrengthenedAmmoAmount >= (_magazineCapacity - _currentStrengthenedMagazineCapacity))
        {
            _totalStrengthenedAmmoAmount -= _magazineCapacity - _currentStrengthenedMagazineCapacity;
            _currentStrengthenedMagazineCapacity = _magazineCapacity;
        }
        else
        {
            _currentStrengthenedMagazineCapacity += _totalStrengthenedAmmoAmount;
            _totalStrengthenedAmmoAmount = 0;
        }
    }


    private void StartReloading()
    {
        _isReloading = true;
        _reloadPanelAnimation.SetActive(true);
    }


    private void NotEnoughAmmo()
    {
        // Some sound
    }


    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(_reloadTime);

        _isReloading = false;
        TurnOffReloadingPanel();
        UpdateValuesOnScreen();
    }


    public float CurrentTimeBetweenShoots { get => _currentTimeBetweenShoots; set => _currentTimeBetweenShoots = value; }

    public float MaxBulletSpreadAngle { get => _maxBulletSpreadAngle; set => _maxBulletSpreadAngle = value; }

    public int CurrentCommonMagazineCapacity { get => _currentCommonMagazineCapacity; set => _currentCommonMagazineCapacity = value; }

    public int TotalCommonAmmoAmount { get => _commonTotalAmmoAmount; set => _commonTotalAmmoAmount = value; }

    public bool IsReloading { get => _isReloading; }

    public int TotalStrengthenedAmmoAmount { get => _totalStrengthenedAmmoAmount; set => _totalStrengthenedAmmoAmount = value; }

    public int CurrentStrengthenedMagazineCapacity { get => _currentStrengthenedMagazineCapacity; set => _currentStrengthenedMagazineCapacity = value; }
}