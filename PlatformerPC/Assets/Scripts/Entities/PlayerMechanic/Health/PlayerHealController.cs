using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _healPurchasedText;
    [SerializeField] private Image[] _fadeImage;

    private PlayerHealthController _playerHealthController;

    private int[] _healPurchased;

    private float[] _currentCooldown;

    private readonly float[] _cooldown = { 3, 7, 15, 30 };
    private readonly float[] _healPower = { 8, 24, 50, 100 };

    private bool[] _isOnCooldown;


    void Start()
    {
        _currentCooldown = new float[_cooldown.Length];
        _isOnCooldown = new bool[_cooldown.Length];
        _playerHealthController = GetComponent<PlayerHealthController>();
        DefaultValues();
    }


    void Update()
    {
        if (!DefaultLevelController.IsGameEnd && !MiniShopController.IsShopping)
        {
            PlayerInput();
        }
        FadeAnimation();
    }


    private void DefaultValues()
    {
        _healPurchased = new int[_healPurchasedText.Length];

        for (int i = 0; i < _healPurchasedText.Length; ++i)
        {
            _healPurchasedText[i].text = _healPurchased[i].ToString();
            _fadeImage[i].fillAmount = 0f;
        }
    }


    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_isOnCooldown[0])
        {
            UseHeal(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !_isOnCooldown[1])
        {
            UseHeal(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !_isOnCooldown[2])
        {
            UseHeal(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !_isOnCooldown[3])
        {
            UseHeal(3);
        }
    }


    private void FadeAnimation()
    {
        for (int i = 0; i < _currentCooldown.Length; ++i)
        {
            if (_isOnCooldown[i] == true)
            {
                _fadeImage[i].fillAmount = _currentCooldown[i] / _cooldown[i];
                _currentCooldown[i] -= Time.deltaTime;
                if (_currentCooldown[i] <= 0)
                {
                    _isOnCooldown[i] = false;
                }
            }
        }
    }


    private void UseHeal(int ID)
    {
        if (_healPurchased[ID] > 0 && _playerHealthController.CurrentHealth != _playerHealthController.MaxHealth && !_isOnCooldown[ID])
        {
            AudioController.Instance.PlayHealSound();

            _healPurchased[ID]--;

            _fadeImage[ID].fillAmount = 1f;
            _currentCooldown[ID] = _cooldown[ID];
            _isOnCooldown[ID] = true;

            _playerHealthController.TakeHeal(_healPower[ID]);
            UpdateValuesOnScreen();
        }
    }


    public void AddHeal(int ID)
    {
        _healPurchased[ID]++;
        UpdateValuesOnScreen();
    }


    private void UpdateValuesOnScreen()
    {
        for (int i = 0; i < _healPurchasedText.Length; ++i)
        {
            _healPurchasedText[i].text = _healPurchased[i].ToString();
        }
    }


    public int[] HealPurchased { get => _healPurchased; }
}