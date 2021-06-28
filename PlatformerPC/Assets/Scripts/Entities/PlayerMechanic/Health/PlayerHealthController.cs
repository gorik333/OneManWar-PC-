using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Text _currentHPNumberText;

    private PlayerAnimationController _animationController;
    private PlayerShieldController _playerShieldController;
    private LoseScreenController _loseScreenController;

    private SpriteRenderer _playerSprite;

    private bool _isAlive = true;

    private float _currentHealth;
    private float _maxHealth;


    void Start()
    {
        DefautltValues();
        _playerSprite = GetComponent<SpriteRenderer>();

        _playerShieldController = GetComponent<PlayerShieldController>();
        _animationController = GetComponent<PlayerAnimationController>();

        _loseScreenController = FindObjectOfType(typeof(LoseScreenController)) as LoseScreenController;
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


    private void UpdateHealthBarInfo()
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
            _playerShieldController.IsActivated = false;
            _playerShieldController.UpdateValuesOnScreen();

            _animationController.StartDeathAnim();
            _currentHPNumberText.text = "0";
            StartCoroutine(LoseScreenDelay());
            _isAlive = false;
        }
    }


    private IEnumerator LoseScreenDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.DISAPPEARANCE_DELAY);
        _loseScreenController.PlayerDied();
    }


    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    public float MaxHealth { get => _maxHealth; }

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
}
