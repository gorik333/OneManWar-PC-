using UnityEngine;
using UnityEngine.UI;

public class FirstLvlPlayerShieldController : MonoBehaviour
{
    [SerializeField] private GameObject _shieldPanel;
    [SerializeField] private GameObject _shieldCollider;

    [SerializeField] private Image _shieldBar;
    [SerializeField] private Text _currentDurabilityText;

    private FirstLvlPlayerAnimationController _animationController;

    private bool _isActivated;

    private float _currentDurability;
    private float _maxDurability;

    private int _amountOfDamageAbsorption;


    void Start()
    {
        _animationController = GetComponent<FirstLvlPlayerAnimationController>();

        DefautltValues();
        UpdateValuesOnScreen();
    }


    private void DefautltValues()
    {
        _shieldCollider.SetActive(false);
        _shieldPanel.gameObject.SetActive(_isActivated);
        _currentDurability = _maxDurability;
    }


    public void UpdateValuesOnScreen()
    {
        _shieldBar.fillAmount = _currentDurability / _maxDurability;
        _currentDurabilityText.text = Mathf.RoundToInt(_currentDurability).ToString();

        _shieldPanel.gameObject.SetActive(_isActivated);
        _shieldCollider.SetActive(_isActivated);
        _animationController.StartShieldAnimation(_isActivated);
    }


    public void UpgradeShield(float durabilityUpgrade, int absorptionUpgrade)
    {
        _shieldCollider.SetActive(true);
        _isActivated = true;

        _maxDurability = durabilityUpgrade + PlayerUpgrades.ExtraShieldDurability.Value;
        _currentDurability = _maxDurability;
        _amountOfDamageAbsorption = absorptionUpgrade;
        UpdateValuesOnScreen();
    }


    public void ChargeShield(int percent)
    {
        _shieldCollider.SetActive(true);
        _isActivated = true;

        if (_currentDurability + (_maxDurability * percent) / 100 > _maxDurability)
        {
            _currentDurability = _maxDurability;
        }
        else
        {
            _currentDurability += (_maxDurability * percent) / 100;
        }

        UpdateValuesOnScreen();
    }


    public float AbsorbDamage(float damage)
    {
        _currentDurability -= damage;

        if (_currentDurability <= 0)
        {
            _shieldCollider.SetActive(false);
            _isActivated = false;
            _currentDurabilityText.text = "0";
            _currentDurability = 0;
        }

        UpdateValuesOnScreen();
        return damage * (100f - _amountOfDamageAbsorption) / 100;
    }


    public float CurrentDurability { get => _currentDurability; set => _currentDurability = value; }

    public float MaxDurability { get => _maxDurability; set => _maxDurability = value; }

    public bool IsActivated { get => _isActivated; set => _isActivated = value; }

    public int AmountOfDamageAbsorb { get => _amountOfDamageAbsorption; }
}