using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

[System.Serializable]
public struct ProgressBars
{
    public GameObject[] BarPoints;
}

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillPointsText;

    [SerializeField] private Sprite _maxLevelSprite;

    [Header("Buttons and upgrade texts")]
    [SerializeField] private GameObject[] _upgradeAdditionText;
    [SerializeField] private GameObject[] _upgradeButtons;

    [Header("Bar points array")]
    public ProgressBars[] _progressBars;


    #region Current upgrade UI value
    [Header("Current upgrade values")]
    [SerializeField] private TextMeshProUGUI _additionalMagazineCapacityText;
    [SerializeField] private TextMeshProUGUI _BonusGoldFromEnemiesText;
    [SerializeField] private TextMeshProUGUI _getSkillPointChanceText;
    [SerializeField] private TextMeshProUGUI _hitPointsText;

    [SerializeField] private TextMeshProUGUI _shieldDurabilityText;
    [SerializeField] private TextMeshProUGUI _fireRateText;
    [SerializeField] private TextMeshProUGUI _additionalDamageText;
    [SerializeField] private TextMeshProUGUI _reloadTimeDecreaseText;
    #endregion

    #region Upgrade variables

    public static ReactiveProperty<int> AdditionalMagazineCapacity = new ReactiveProperty<int>();
    public static ReactiveProperty<int> BonusGoldFromEnemies = new ReactiveProperty<int>();
    public static ReactiveProperty<int> GetSkillPointChance = new ReactiveProperty<int>();
    public static ReactiveProperty<int> ExtraHitPoints = new ReactiveProperty<int>();

    public static ReactiveProperty<int> ExtraShieldDurability = new ReactiveProperty<int>();
    public static ReactiveProperty<int> AdditionalDamage = new ReactiveProperty<int>();

    public static ReactiveProperty<float> AdditionalFireRate = new ReactiveProperty<float>();
    public static ReactiveProperty<float> ReloadTimeDecrease = new ReactiveProperty<float>();

    #endregion

    public static ReactiveProperty<int> SkillPoints = new ReactiveProperty<int>(0);
    

    void Awake()
    {
        RestoreProgressBars();
    }


    void Start()
    {
        DefaultValues();
    }


    private void DefaultValues()
    {
        if (GetSkillPointChance.Value == 0)
        {
            GetSkillPointChance.Value = GlobalDefVals.START_SKILLPOINT_CHANCE;
        }

        SkillPoints.Subscribe(t => _skillPointsText.text = t.ToString());

        ExtraHitPoints.Subscribe(t => _hitPointsText.text = t.ToString());
        AdditionalMagazineCapacity.Select(AMC => (AMC + GlobalDefVals.MAGAZINE_CAPACITY).ToString()).Subscribe(t => _additionalMagazineCapacityText.text = t);
        BonusGoldFromEnemies.Subscribe(t => _BonusGoldFromEnemiesText.text = t.ToString());
        GetSkillPointChance.Subscribe(t => _getSkillPointChanceText.text = t.ToString() + "%");
        AdditionalFireRate.Select(AFR => (AFR + GlobalDefVals.FIRE_RATE).ToString()).Subscribe(t => _fireRateText.text = t);
        ExtraShieldDurability.Subscribe(t => _shieldDurabilityText.text = t.ToString());
        ReloadTimeDecrease.Select(RTD => (GlobalDefVals.PLAYER_RELOAD_TIME - RTD).ToString()).Subscribe(t => _reloadTimeDecreaseText.text = t);
        AdditionalDamage.Subscribe(t => _additionalDamageText.text = t.ToString());
    }


    public void UpgradeOnClick(int ID)
    {
        if (SkillPoints.Value > 0)
        {
            AudioController.Instance.PlayUpgradeSound();

            SkillPoints.Value -= 1;
            FillProgressBar(ID);
            UpgradeValue(ID);
            SaveDataController.SavePlayerUpgradesData();
        }
        else
        {
            AudioController.Instance.PlayUnsuccessfulPurchaseSound();
        }
    }


    private void FillProgressBar(int ID)
    {
        for (int i = 0; i < _progressBars[ID].BarPoints.Length; ++i)
        {
            if (!_progressBars[ID].BarPoints[i].activeInHierarchy)
            {
                _progressBars[ID].BarPoints[i].SetActive(true);

                SaveDataController.ActiveBars[ID] = i + 1; // plus 1 because "loop for" starts at 0

                if (i == 9)
                {
                    CheckIfMaxUpgrade(ID);
                }
                break;
            }
        }
    }


    private void RestoreProgressBars()
    {
        for (int i = 0; i < _progressBars.Length; ++i)
        {
            for (int j = 0; j < _progressBars[i].BarPoints.Length; ++j)
            {
                if (SaveDataController.ActiveBars[i] > j)
                {
                    _progressBars[i].BarPoints[j].SetActive(true);

                    if (j == 9)
                    {
                        CheckIfMaxUpgrade(i);
                    }
                }
            }
        }
    }


    private void CheckIfMaxUpgrade(int ID)
    {
        DisableButtonAndChangeSprite(ID);
        Destroy(_upgradeAdditionText[ID]); // Remove green text with upgrade value
    }


    private void DisableButtonAndChangeSprite(int ID)
    {
        _upgradeButtons[ID].GetComponent<Button>().interactable = false;
        var buttonImage = _upgradeButtons[ID].GetComponent<Image>();
        buttonImage.sprite = _maxLevelSprite;
        buttonImage.color = Color.white;
    }


    private void UpgradeValue(int ID)
    {
        switch (ID)
        {
            case 0:
                ExtraHitPoints.Value += 5;
                break;
            case 1:
                ExtraShieldDurability.Value += 10;
                break;
            case 2:
                GetSkillPointChance.Value += 1;
                break;
            case 3:
                AdditionalFireRate.Value += 0.04f;
                break;
            case 4:
                ReloadTimeDecrease.Value += 0.08f;
                break;
            case 5:
                AdditionalMagazineCapacity.Value += 1;
                break;
            case 6:
                BonusGoldFromEnemies.Value += 2;
                break;
            case 7:
                AdditionalDamage.Value += 1;
                break;
        }
    }
}
