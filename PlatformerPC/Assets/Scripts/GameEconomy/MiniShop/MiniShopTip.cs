using UnityEngine;

public class MiniShopTip : MonoBehaviour
{
    [SerializeField] private GameObject _openShopTipPanel;
    [SerializeField] private GameObject _bottomHealPanel;
    [SerializeField] private GameObject _purchaseHealPanel;
    [SerializeField] private GameObject _hotkeysTipPanel;

    [SerializeField] private GameObject _makePlayerBuyHealPanel;
    [SerializeField] private GameObject _closeShopTipPanel;
    [SerializeField] private GameObject _tipBeforeBuyPanel;
    [SerializeField] private GameObject _fadeImage;

    [SerializeField] private GameObject _tryAnotherTip;

    [SerializeField] private Canvas _lvlOneHealCanvas;

    [SerializeField] private Animator _arrowAnimator;

    private FirstLvlPlayerHealController _playerHealController;

    private bool _isEntered;
    private bool _isPurchased;


    void Start()
    {
        _playerHealController = FindObjectOfType(typeof(FirstLvlPlayerHealController)) as FirstLvlPlayerHealController;

        if (FirstLvlTipsController.AllAllow)
        {
            _bottomHealPanel.SetActive(true);
            Destroy(gameObject);
        }

        DefaultValues();
    }


    void Update()
    {
        OpenAndCloseInputs();
        if (!_isPurchased)
        {
            for (int i = 0; i < _playerHealController.HealPurchased.Length; ++i)
            {
                if (_playerHealController.HealPurchased[i] != 0)
                {
                    UnlockHealPanel();
                    _closeShopTipPanel.LeanScale(Vector2.one, 0.3f);
                }
            }
            if (_purchaseHealPanel.activeSelf)
            {
                _tipBeforeBuyPanel.SetActive(false);
                _arrowAnimator.SetBool("OnHeal", false);
                _arrowAnimator.SetBool("OnBuy", true);
            }
        }
    }


    private void DefaultValues()
    {
        _closeShopTipPanel.SetActive(true);
        _closeShopTipPanel.transform.localScale = Vector2.zero;

        _tipBeforeBuyPanel.SetActive(true);
        _makePlayerBuyHealPanel.SetActive(true);

        _tipBeforeBuyPanel.transform.localScale = Vector2.zero;
        _makePlayerBuyHealPanel.transform.localScale = Vector2.zero;

        _openShopTipPanel.SetActive(false);
        _arrowAnimator.gameObject.SetActive(false);
    }


    private void OpenAndCloseInputs()
    {

        if (_isEntered && Input.GetKeyDown(KeyCode.B) && Time.timeScale != 0)
        {
            EnterToTheShop();
        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0)
        {
            FirstLvlPlayerInput._isPopUpDialogActive = false;
            DefaultValues();
            Destroy(transform.parent.gameObject);
        }
    }


    private void TurnBuyHealTipOn()
    {
        _tipBeforeBuyPanel.transform.LeanScale(Vector2.one, 0.2f);
        _makePlayerBuyHealPanel.transform.LeanScale(Vector2.one, 0.2f);

        _fadeImage.SetActive(true);
        _arrowAnimator.gameObject.SetActive(true);
        _arrowAnimator.SetBool("OnHeal", true);
    }


    public void EnterToTheShop()
    {
        _openShopTipPanel.SetActive(false);

        if (!_isPurchased)
        {
            TurnBuyHealTipOn();
        }
    }


    public void TurnOpenShopTipOn()
    {
        FirstLvlPlayerInput._isPopUpDialogActive = true;
        _openShopTipPanel.SetActive(true);
    }


    public void TurnOpenShopTipOff()
    {
        FirstLvlPlayerInput._isPopUpDialogActive = false;
        _openShopTipPanel.SetActive(false);
    }


    public void CloseOnClick()
    {
        SoundOnButtonClick();
        FirstLvlPlayerInput._isPopUpDialogActive = false;
        Destroy(transform.parent.gameObject);
    }


    private void UnlockHealPanel()
    {
        _lvlOneHealCanvas.sortingOrder = 1;
        _hotkeysTipPanel.SetActive(true);
        _tryAnotherTip.SetActive(true);
        _arrowAnimator.gameObject.SetActive(false);
        _bottomHealPanel.SetActive(true);
        _isPurchased = true;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            TurnOpenShopTipOn();
            _isEntered = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            TurnOpenShopTipOff();
            _isEntered = false;
            FirstLvlPlayerInput._isPopUpDialogActive = false;
        }
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}