using UnityEngine;
using TMPro;

public class BuyShieldController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _extraDurabilityText;
    [SerializeField] private TextMeshProUGUI[] _chargersPriceText;

    [Header("Shield description")]
    [SerializeField] private TextMeshProUGUI _shieldPriceText;
    [SerializeField] private TextMeshProUGUI _shieldDurabilityText;
    [SerializeField] private TextMeshProUGUI _shieldDamageAbsorptionText;
    [SerializeField] private TextMeshProUGUI _currentShieldLevelText;

    [SerializeField] private TextMeshProUGUI _maxLevelShieldDurabilityText;

    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private GameObject _maxLevelImage;
    [SerializeField] private GameObject _chargersPanel;

    [SerializeField] private GameObject _maxLevelDescriptionPanel;
    [SerializeField] private GameObject _levelDescriptionPanel;

    private PlayerShieldController _playerShieldController;
    private FirstLvlPlayerShieldController _firstLvlPlayerShieldController;


    private readonly int[] _chargerPrice = { 15, 22, 36, 50 };
    private readonly int[] _chargerPower = { 35, 50, 75, 100 }; // percent restores

    private readonly float[] _shieldDurability = GlobalDefVals.SHIELD_DURABILITY_ARRAY;

    private readonly int[] _shieldDamageAbsorption = { 30, 40, 50, 60, 80 };
    private readonly int[] _shieldPrice = GlobalDefVals.SHIELD_PRICE_ARRAY;

    private int _currentShieldLevel;


    void Start()
    {
        _playerShieldController = FindObjectOfType(typeof(PlayerShieldController)) as PlayerShieldController;
        _firstLvlPlayerShieldController = FindObjectOfType(typeof(FirstLvlPlayerShieldController)) as FirstLvlPlayerShieldController;

        DefaultValues();
    }


    private void DefaultValues()
    {
        _chargersPanel.SetActive(false);
        _currentShieldLevel = 0;
        for (int i = 0; i < _chargersPriceText.Length; i++)
        {
            _chargersPriceText[i].text = _chargerPrice[i].ToString();
            if (i < _extraDurabilityText.Length)
            {
                _extraDurabilityText[i].text = "+" + PlayerUpgrades.ExtraShieldDurability.ToString();
            }
        }
    }


    public void BuyOrUpgradeShieldOnClick()
    {
        if (_currentShieldLevel < 5)
        {
            bool isPurchased = DefaultLevelController.BuySomething(_shieldPrice[_currentShieldLevel]);

            if (isPurchased)
            {
                if (_playerShieldController != null)
                {
                    _playerShieldController.UpgradeShield(_shieldDurability[_currentShieldLevel], _shieldDamageAbsorption[_currentShieldLevel]);
                }
                else
                {
                    _firstLvlPlayerShieldController.UpgradeShield(_shieldDurability[_currentShieldLevel], _shieldDamageAbsorption[_currentShieldLevel]);
                }
                _currentShieldLevel++;

                UpgradeDescriptionOnScreen();
                UpgradeChargerPricesOnScreen();

                if (_currentShieldLevel == 1)
                {
                    Destroy(_buyButton);
                    _chargersPanel.SetActive(true);
                    _upgradeButton.SetActive(true);
                }
                if (_currentShieldLevel >= 5)
                {
                    Destroy(_upgradeButton);
                    _maxLevelImage.SetActive(true);
                    _levelDescriptionPanel.SetActive(false);
                    _maxLevelShieldDurabilityText.text = GlobalDefVals.SHIELD_DURABILITY_ARRAY[4].ToString();
                    _maxLevelDescriptionPanel.SetActive(true);
                }
                SuccessfulPurchaseSound();
            }
            else
            {
                UnsuccessfulPurchaseSound(); ;
            }

        }
    }


    private void UpgradeChargerPricesOnScreen()
    {
        for (int i = 0; i < _chargersPriceText.Length; i++)
        {
            _chargersPriceText[i].text = (_chargerPrice[i] * _currentShieldLevel).ToString();
        }
    }


    private void UpgradeDescriptionOnScreen()
    {
        if (_currentShieldLevel < 5)
        {
            _shieldPriceText.text = _shieldPrice[_currentShieldLevel].ToString();
            _shieldDurabilityText.text = _shieldDurability[_currentShieldLevel].ToString();
            _shieldDamageAbsorptionText.text = _shieldDamageAbsorption[_currentShieldLevel].ToString();
        }
        else
        {
            _shieldPriceText.text = "0";
        }
        _currentShieldLevelText.text = (_currentShieldLevel).ToString();
    }



    public void BuyChargerOnClick(int ID)
    {
        if (_playerShieldController != null)
        {
            if (_playerShieldController.CurrentDurability != _playerShieldController.MaxDurability)
            {
                bool isPurchased = DefaultLevelController.BuySomething(_chargerPrice[ID] * _currentShieldLevel);

                if (isPurchased)
                {
                    _playerShieldController.ChargeShield(_chargerPower[ID]);
                    ChargeShieldSound();
                    SuccessfulPurchaseSound();
                }
                else
                {
                    UnsuccessfulPurchaseSound();
                }
            }
            else
            {
                UnsuccessfulPurchaseSound();
            }
        }
        else
        {
            if (_firstLvlPlayerShieldController.CurrentDurability != _firstLvlPlayerShieldController.MaxDurability)
            {
                bool isPurchased = DefaultLevelController.BuySomething(_chargerPrice[ID] * _currentShieldLevel);

                if (isPurchased)
                {
                    _firstLvlPlayerShieldController.ChargeShield(_chargerPower[ID]);
                    ChargeShieldSound();
                    SuccessfulPurchaseSound();
                }
                else
                {
                    UnsuccessfulPurchaseSound();
                }
            }
            else
            {
                UnsuccessfulPurchaseSound();
            }
        }
    }


    private void ChargeShieldSound()
    {
        AudioController.Instance.PlayChargeShieldSound();
    }


    private void SuccessfulPurchaseSound()
    {
        AudioController.Instance.PlaySuccessfulPurchaseSound();

    }


    private void UnsuccessfulPurchaseSound()
    {
        AudioController.Instance.PlayUnsuccessfulPurchaseSound();
    }
}