using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstLvlPlayerHealthController : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Text _currentHPNumberText;

    private FirstLvlPlayerAnimationController _animationController;
    private FirstLvlPlayerShieldController _playerShieldController;
    private FirstLvlCheckPointsMaster _checkPointsMaster;
    private FirstLvlTipsController _tipsController;
    private LoseScreenController _loseScreenController;

    private bool _isAlive = true;

    private float _currentHealth;
    private float _maxHealth;


    void Start()
    {
        _playerShieldController = GetComponent<FirstLvlPlayerShieldController>();
        _animationController = GetComponent<FirstLvlPlayerAnimationController>();

        _tipsController = FindObjectOfType(typeof(FirstLvlTipsController)) as FirstLvlTipsController;
        _checkPointsMaster = FindObjectOfType(typeof(FirstLvlCheckPointsMaster)) as FirstLvlCheckPointsMaster;
        _loseScreenController = FindObjectOfType(typeof(LoseScreenController)) as LoseScreenController;

        DefautltValues();
    }


    private void DefautltValues()
    {
        _maxHealth = GlobalDefVals.MAX_HIT_POINTS + PlayerUpgrades.ExtraHitPoints.Value;
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
        _currentHPNumberText.text = Mathf.RoundToInt(_currentHealth).ToString();
    }


    public void TakeHeal(float value)
    {
        if (_currentHealth + value <= _maxHealth)
        {
            _currentHealth += value;
        }
        else
        {
            _currentHealth = _maxHealth;
        }
        UpdateHealthBarInfo();
    }


    public void UpdateHealthBarInfo()
    {
        _healthBar.color = new Color(1f - _currentHealth / _maxHealth, _currentHealth / _maxHealth, 0); // псевдо переход с зеленого здоровья к красному
        _healthBar.fillAmount = _currentHealth / _maxHealth;
        _currentHPNumberText.text = Mathf.RoundToInt(_currentHealth).ToString();
    }


    public void TakeDamage(float damage, bool isFromTheFront)
    {
        if (_isAlive)
        {
            if (!_playerShieldController.IsActivated || !isFromTheFront) // Тут можно сделать меньше проверок, но для читаемости лучше оставить.
            {
                _currentHealth -= damage;
            }
            else if (_playerShieldController.IsActivated && isFromTheFront)
            {
                _currentHealth -= _playerShieldController.AbsorbDamage(damage);
            }
            UpdateHealthBarInfo();
        }
        CheckIfAlive();
    }


    private void CheckIfAlive()
    {
        if (_currentHealth <= 0 && _isAlive)
        {
            _animationController.StartDeathAnim();
            _currentHPNumberText.text = "0";
            _tipsController.TurnOnRemider();

            _playerShieldController.IsActivated = false;
            _playerShieldController.UpdateValuesOnScreen();

            if (!FirstLvlTipsController.AllAllow)
            {
                StartCoroutine(RespawnDelay());
            }
            else
            {
                StartCoroutine(LoseScreenDelay());
            }
            _isAlive = false;
        }
    }


    private IEnumerator LoseScreenDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.DISAPPEARANCE_DELAY);
        _loseScreenController.PlayerDied();
    }


    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.RESPAWN_DELAY);
        _isAlive = true;
        _checkPointsMaster.RespawnOnLastCheckPoint();
        _animationController.Respawn();
    }


    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    public float MaxHealth { get => _maxHealth; }

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
}
