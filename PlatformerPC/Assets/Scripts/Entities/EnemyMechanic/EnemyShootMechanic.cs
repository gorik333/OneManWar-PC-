using UnityEngine;
using System.Collections;

public class EnemyShootMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _commonBullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _enemyBody;

    [SerializeField] private float outcomeDamage;
    [SerializeField] private float _fireRate;

    private EnemyHealthController _enemyHealthController;
    private EnemyAnimationController _enemyAnimationController;

    private float _maxBulletSpreadAngle;
    private float _bulletSpeed;

    private float _totalCommonAmmoAmount;
    private float _currentMagazineCapacity;
    private float _timeBetweenShoots;
    private float _currentTimeBetweenShoots = 0f;

    private bool _isReloading;


    void Start()
    {
        _enemyHealthController = GetComponent<EnemyHealthController>();
        _enemyAnimationController = GetComponent<EnemyAnimationController>();
        DefaultValues();
    }


    void DefaultValues()
    {
        _maxBulletSpreadAngle = GlobalDefVals.ENEMY_MAX_BULLET_SPREAD_ANGLE;
        _timeBetweenShoots = GlobalDefVals.ENEMY_TIME_BETWEEN_SHOTS;
        _bulletSpeed = GlobalDefVals.COMMON_BULLET_SPEED;
        _totalCommonAmmoAmount = GlobalDefVals.TOTAL_COMMON_AMMO;
        _currentMagazineCapacity = GlobalDefVals.MAGAZINE_CAPACITY;

        if (_fireRate == 0)
        {
            _fireRate = GlobalDefVals.FIRE_RATE;
        }
    }


    void Update()
    {
        _currentTimeBetweenShoots += Time.deltaTime;
    }


    public void Shoot()
    {
        if (_currentTimeBetweenShoots >= (_timeBetweenShoots / _fireRate) && _enemyHealthController.IsAlive && !_isReloading)
        {
            SpawnCommonBullet();

            AudioController.Instance.PlayEnemyCommonShotSound();

            _currentTimeBetweenShoots = 0f;
            _enemyAnimationController.StartShootAnim();
            _currentMagazineCapacity--;

            if (_currentMagazineCapacity <= 0)
            {
                Reload();
            }
        }
    }

    
    private void SpawnCommonBullet()
    {
        Quaternion spread = Quaternion.Euler(_enemyBody.rotation.eulerAngles + new Vector3(0f, 0f, Random.Range(-_maxBulletSpreadAngle, _maxBulletSpreadAngle)));
        GameObject currentBullet = Instantiate(_commonBullet, _firePoint.position, spread);

        if (_enemyBody.rotation.y == 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * _bulletSpeed);
        }
        else if (_enemyBody.rotation.y != 0)
        {
            currentBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * _bulletSpeed);
        }

        var currentBulletDamage = currentBullet.GetComponent<EnemyBulletMechanic>();

        if (currentBulletDamage == null)
        {
            var currentTutorialBulletDamage = currentBullet.GetComponent<FirstLvlEnemyBulletMechanic>(); // It's created for tutorial lvl, instead creating new classes for enemy
            currentTutorialBulletDamage.Damage = outcomeDamage;
        }
        else
        {
            currentBulletDamage.Damage = outcomeDamage;
        }
    }


    private void Reload()
    {
        _isReloading = true;
        if (_totalCommonAmmoAmount >= (GlobalDefVals.MAGAZINE_CAPACITY - _currentMagazineCapacity))
        {
            AudioController.Instance.PlayEnemyReloadSound();
            StartCoroutine(ReloadDelay());
        }
    }


    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.ENEMY_RELOAD_TIME);
        _totalCommonAmmoAmount -= (GlobalDefVals.MAGAZINE_CAPACITY - _currentMagazineCapacity);
        _currentMagazineCapacity = GlobalDefVals.MAGAZINE_CAPACITY;
        _isReloading = false;
    }


    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }

    public float OutcomeDamage { get => outcomeDamage; set => outcomeDamage = value; }

    public float MaxBulletSpreadAngle { get => _maxBulletSpreadAngle; set => _maxBulletSpreadAngle = value; }
}
