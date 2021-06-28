using UnityEngine;
using UnityEngine.UI;

public class MiniShopController : MonoBehaviour
{
    [SerializeField] private GameObject _shop;

    [SerializeField] private Button[] _button;

    private bool _isEnteredToZone;

    private static bool _isShopping;


    void Start()
    {
        _shop.transform.localScale = Vector2.zero;
        SetDefaultValues();
    }


    void Update()
    {
        if (_isEnteredToZone && Input.GetKeyDown(KeyCode.B) && Time.timeScale != 0 && !_isShopping)
        {
            OpenShop();
        }
        if (_shop.activeSelf && Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0 && _isShopping)
        {
            CloseShop();
        }
    }


    void SetDefaultValues()
    {
        for (int i = 0; i < _button.Length; ++i)
        {
            if (_button[i].name.Equals("AmmoPanelButton"))
            {
                _button[i].interactable = false;
                continue;
            }
            _button[i].interactable = true;
        }
    }


    private void OpenShop()
    {
        _shop.SetActive(true);
        _shop.transform.LeanScale(Vector2.one, 0.15f);
        _isShopping = true;
    }


    public void CloseShop()
    {
        _shop.transform.localScale = Vector2.zero;
        AmmunitionPanelController.Instance.HideAllInfo();

        _isShopping = false;
        _shop.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody"))
        {
            AudioController.Instance.PlayShopEntranceSound();
            _isEnteredToZone = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            _isEnteredToZone = false;
        }
    }


    public static bool IsShopping { get => _isShopping; }
}
