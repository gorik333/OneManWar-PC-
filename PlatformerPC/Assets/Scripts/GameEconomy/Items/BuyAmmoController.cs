using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BuyAmmoController : MonoBehaviour
{
    [Header("AmmoPrice")]
    [SerializeField] private TextMeshProUGUI _commonAmmoPriceText;
    [SerializeField] private TextMeshProUGUI _strengthenedPriceText;

    [Header("AmmoInputField")]
    [SerializeField] private InputField _commonAmmoField;
    [SerializeField] private InputField _strengthenedAmmoField;

    private PlayerShootMechanic _playerShootMechanic;
    private FirstLvlPlayerShootMechanic _firstLvlPlayerShootMechanic;

    private int _commonAmmoEntered;
    private int _strengthenedAmmoEntered;

    private int _commonAmmoPrice;
    private int _strengthenedAmmoPrice;

    private int _totalCommonAmmoPrice;
    private int _totalStrengthenedAmmoPrice;


    void Awake()
    {
        _playerShootMechanic = FindObjectOfType(typeof(PlayerShootMechanic)) as PlayerShootMechanic;
        _firstLvlPlayerShootMechanic = FindObjectOfType(typeof(FirstLvlPlayerShootMechanic)) as FirstLvlPlayerShootMechanic;

        SetDefaultValues();
    }


    void Update()
    {
        UpdateCommonBulletValues();
        UpdateStrengthenedBulletValues();
    }


    private void SetDefaultValues()
    {
        _commonAmmoEntered = 1;
        _strengthenedAmmoEntered = 1;

        _commonAmmoPrice = GlobalDefVals.COMMON_AMMO_PRICE;
        _strengthenedAmmoPrice = GlobalDefVals.STRENGTHENED_AMMO_PRICE;

        _commonAmmoPriceText.text = _commonAmmoPrice.ToString();
        _strengthenedPriceText.text = _strengthenedAmmoPrice.ToString();

        _commonAmmoField.text = _commonAmmoEntered.ToString();
        _strengthenedAmmoField.text = _strengthenedAmmoEntered.ToString();
    }


    public void ChangeCommonBulletsCountOnClick(int number)
    {
        _commonAmmoEntered += number;
        _commonAmmoField.text = _commonAmmoEntered.ToString();
    }


    public void ChangeStrengthenedBulletsCountOnClick(int number)
    {
        _strengthenedAmmoEntered += number;
        _strengthenedAmmoField.text = _strengthenedAmmoEntered.ToString();
    }


    public void BuyCommonBulletsOnClick()
    {
        bool isPurchased = DefaultLevelController.BuySomething(_totalCommonAmmoPrice);

        if (isPurchased)
        {
            if (_playerShootMechanic != null)
            {
                _playerShootMechanic.AddCommonAmmo(_commonAmmoEntered);
            }
            else
            {
                _firstLvlPlayerShootMechanic.AddCommonAmmo(_commonAmmoEntered);
            }
            AudioController.Instance.PlaySuccessfulPurchaseSound();
        }
        else
        {
            AudioController.Instance.PlayUnsuccessfulPurchaseSound();
        }
    }


    public void BuyStrengthenedBulletsOnClick()
    {
        bool isPurchased = DefaultLevelController.BuySomething(_totalStrengthenedAmmoPrice);

        if (isPurchased)
        {
            if (_playerShootMechanic != null)
            {
                _playerShootMechanic.AddStrengthenedAmmo(_strengthenedAmmoEntered);
            }
            else
            {
                _firstLvlPlayerShootMechanic.AddStrengthenedAmmo(_strengthenedAmmoEntered);
            }
            AudioController.Instance.PlaySuccessfulPurchaseSound();
        }
        else
        {
            AudioController.Instance.PlayUnsuccessfulPurchaseSound();
        }
    }


    private void UpdateCommonBulletValues()
    {
        if (Convert.ToInt32(_commonAmmoField.text) > 99)
        {
            _commonAmmoField.text = "99";
        }
        if (Convert.ToInt32(_commonAmmoField.text) <= 0)
        {
            _commonAmmoField.text = "1";
        }

        _commonAmmoEntered = Convert.ToInt32(_commonAmmoField.text);

        _totalCommonAmmoPrice = _commonAmmoPrice * _commonAmmoEntered;

        _commonAmmoPriceText.text = _totalCommonAmmoPrice.ToString();
    }


    private void UpdateStrengthenedBulletValues()
    {
        if (Convert.ToInt32(_strengthenedAmmoField.text) > 99)
        {
            _strengthenedAmmoField.text = "99";
        }
        if (Convert.ToInt32(_strengthenedAmmoField.text) <= 0)
        {
            _strengthenedAmmoField.text = "1";
        }

        _strengthenedAmmoEntered = Convert.ToInt32(_strengthenedAmmoField.text);

        _totalStrengthenedAmmoPrice = _strengthenedAmmoPrice * _strengthenedAmmoEntered;

        _strengthenedPriceText.text = _totalStrengthenedAmmoPrice.ToString();
    }


    public int CommonAmmoPrice { get => _commonAmmoPrice; set => _commonAmmoPrice = value; }

    public int StrengthenedAmmoPrice { get => _strengthenedAmmoPrice; set => _strengthenedAmmoPrice = value; }
}
