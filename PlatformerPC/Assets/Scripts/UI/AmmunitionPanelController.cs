using UnityEngine;
using TMPro;

public class AmmunitionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _hideButton;

    [Header("Info panels")]
    [SerializeField] private GameObject _commonInfoPanel;
    [SerializeField] private GameObject _strengthenedInfoPanel;

    [Header("Additional damage")]
    [SerializeField] private TextMeshProUGUI _commonAdditionalDamage;
    [SerializeField] private TextMeshProUGUI _strengthenedAdditionalDamage;

    public static AmmunitionPanelController Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        int dmg = PlayerUpgrades.AdditionalDamage.Value;

        _commonAdditionalDamage.text = "+" + dmg.ToString();
        _strengthenedAdditionalDamage.text = "+" + (dmg - (dmg / 100 * GlobalDefVals.STRENGTHENED_AMMO_DECREASE)).ToString();

        StrengthenedDefaultState();
        CommonDefaultState();
    }


    public void OpenCommonAmmoInfoOnClick()
    {
        if (_commonInfoPanel.transform.localScale == Vector3.zero)
        {
            _hideButton.SetActive(true);

            _commonInfoPanel.LeanScale(Vector3.one, 0.1f);
            _commonInfoPanel.LeanMoveLocal(new Vector2(20.12f, 19.522f), 0.1f);
        }
        else
        {
            CommonDefaultState();
        }
    }


    public void OpenStrengthenedAmmoInfoOnClick()
    {
        if (_strengthenedInfoPanel.transform.localScale == Vector3.zero)
        {
            _hideButton.SetActive(true);

            _strengthenedInfoPanel.LeanScale(Vector3.one, 0.1f);
            _strengthenedInfoPanel.LeanMoveLocal(new Vector2(20.12f, 19.522f), 0.1f);
        }
        else
        {
            StrengthenedDefaultState();
        }
    }


    public void HideAllInfo()
    {
        CommonDefaultState();
        StrengthenedDefaultState();
    }


    private void CommonDefaultState()
    {
        _commonInfoPanel.LeanScale(Vector3.zero, 0.1f);
        _commonInfoPanel.LeanMoveLocal(new Vector2(-141.5f, -23.1f), 0.1f);

        _hideButton.SetActive(false);
    }


    private void StrengthenedDefaultState()
    {
        _strengthenedInfoPanel.LeanScale(Vector3.zero, 0.1f);
        _strengthenedInfoPanel.LeanMoveLocal(new Vector2(-141.5f, -23.1f), 0.1f);

        _hideButton.SetActive(false);
    }
}
