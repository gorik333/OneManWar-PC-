using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BayatGames.SaveGameFree;

public class FirstLvlTipsController : MonoBehaviour
{
    [Header ("Tip panels")]
    [SerializeField] private GameObject _moveControlTip;
    [SerializeField] private GameObject _jumpControlTip;
    [SerializeField] private GameObject _rotateCameraTip;
    [SerializeField] private GameObject _fireControlTip;
    [SerializeField] private GameObject _balanceTip;
    [SerializeField] private GameObject _jumpPlarformTip;
    [SerializeField] private GameObject _ammoTypesTip;

    [Header ("Game interface panels")]
    [SerializeField] private GameObject _balancePanel;
    [SerializeField] private GameObject _ammoPanel;
    [SerializeField] private GameObject _hitPointsPanel;

    [Header ("Other")]
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private GameObject _pauseScript;
    [SerializeField] private GameObject _playerReady;
    [SerializeField] private GameObject _oneLifeReminder;

    [SerializeField] private Button _pauseButton;

    [SerializeField] private Transform _rotatedBody;

    private FirstLvlPlayerInput _firstLvlPlayerInput;

    private bool _isMoveButtonsPressed;
    private bool _isSpaceButtonPressed;
    private bool _isCameraRotated;
    private bool _isBalanceTipFirstTime;

    private bool _isFirstFallTriggerEnter;
    private bool _isChangeAmmoTypeButtonPressed;

    public static bool AllAllow; // default false


    void Awake()
    {
        _firstLvlPlayerInput = GetComponent<FirstLvlPlayerInput>();

        if (SaveGame.Exists("CurrentDefaultLevelSaved"))
        {
            if (SaveGame.Load<int>("CurrentDefaultLevelSaved") > 0)
            {
                AllAllow = true;
            }
            else
            {
                AllAllow = false;
            }
        }
        
        if (!AllAllow)
        {
            StartCoroutine(StartTipDelay());
        }
        else
        {
            AllAllowMode();
        }
    }

    /// <summary>
    /// When complete first lvl
    /// </summary>
    public void AllAllowMode()
    {
        _fadeImage.SetActive(false);

        _balancePanel.SetActive(true);
        _ammoPanel.SetActive(true);
        _hitPointsPanel.SetActive(true);

        _firstLvlPlayerInput.IsReadyToJump = true;
        _firstLvlPlayerInput.IsReadyToLookAtMouseCursor = true; 
        _firstLvlPlayerInput.IsReadyToFire = true;
    }


    void Update()
    {
        if (!AllAllow)
        {
            MoveCheck();
            JumpCheck();
            RotateCameraCheck();
            ChangeAmmoTypeCheck();
        }
    }


    private void MoveCheck()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftControl)) && !_isMoveButtonsPressed)
        {
            TurnOffFadeImage();
            _moveControlTip.SetActive(false);
            _isMoveButtonsPressed = true;
        }
    }


    private void JumpCheck()
    {
        if (Input.GetKey(KeyCode.Space) && _firstLvlPlayerInput.IsReadyToJump && !_isSpaceButtonPressed)
        {
            TurnOffFadeImage();
            _jumpControlTip.SetActive(false);
            _isSpaceButtonPressed = true;
        }
    }


    private void RotateCameraCheck()
    {
        if (_rotatedBody.rotation.y != 0 && !_isCameraRotated)
        {
            TurnOffFadeImage();
            _rotateCameraTip.SetActive(false);
            _isCameraRotated = true;
        }
    }


    private void ChangeAmmoTypeCheck()
    {
        if (Input.GetKey(KeyCode.C) && !_isChangeAmmoTypeButtonPressed)
        {
            _isChangeAmmoTypeButtonPressed = true;
            StartCoroutine(AmmoTypesTipDelay());
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!AllAllow)
        {
            if (collider.name.Equals("JumpTipTrigger"))
            {
                Destroy(collider.gameObject);
                _firstLvlPlayerInput.IsReadyToJump = true;
                _jumpControlTip.SetActive(true);
                TurnOnFadeImage();
            }
            if (collider.name.Equals("RotateCameraTipTrigger"))
            {
                Destroy(collider.gameObject);
                _firstLvlPlayerInput.IsReadyToLookAtMouseCursor = true;
                _rotateCameraTip.SetActive(true);
                TurnOnFadeImage();
            }
            if (collider.name.Equals("FireTipTrigger"))
            {
                Destroy(collider.gameObject);
                _ammoPanel.SetActive(true);
                _hitPointsPanel.SetActive(true);
                _fireControlTip.SetActive(true);
                TurnOnFadeImage();
            }
            if (collider.name.Equals("JumpPlatformTipTrigger"))
            {
                Destroy(collider.gameObject);
                _jumpPlarformTip.SetActive(true);
                TurnOnFadeImage();
            }
            if (collider.name.Equals("PlayerReadyTrigger"))
            {
                Destroy(collider.gameObject);
                _playerReady.SetActive(true);
                TurnOnFadeImage();
            }
            if (collider.name.Equals("FallTrigger"))
            {
                TurnOnRemider();
            }
            if (collider.name.Equals("FinishCollider"))
            {
                SaveGame.Save("AllAllow", 1);
            }
        }
    }


    public void TurnBalanceTipOn()
    {
        if (!_isBalanceTipFirstTime && !AllAllow)
        {
            _balanceTip.SetActive(true);
            _balancePanel.SetActive(true);
            _isBalanceTipFirstTime = true;
            TurnOnFadeImage();
        }
    }


    public void TurnOnRemider()
    {
        if (!AllAllow && !_isFirstFallTriggerEnter)
        {
            _oneLifeReminder.SetActive(true);
            _isFirstFallTriggerEnter = true;
            TurnOnFadeImage();
        }
    }


    public void CloseTipOnClick(GameObject tip)
    {
        SoundOnButtonClick();
        Destroy(tip);
        TurnOffFadeImage();
    }


    public void CloseFireTipOnClick()
    {
        SoundOnButtonClick();
        _firstLvlPlayerInput.IsReadyToFire = true;
        _fireControlTip.SetActive(false);
        TurnOffFadeImage();
    }


    private void TurnOnFadeImage()
    {
        _fadeImage.SetActive(true);
        _pauseButton.interactable = false;
        _pauseScript.SetActive(false);
        Time.timeScale = 0.1f; // 0.1f default
        FirstLvlPlayerInput._isPopUpDialogActive = true;
    }


    private void TurnOffFadeImage()
    {
        _pauseScript.SetActive(true);
        _pauseButton.interactable = true;
        _fadeImage.SetActive(false);
        Time.timeScale = 1f;
        FirstLvlPlayerInput._isPopUpDialogActive = false;
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }


    private IEnumerator AmmoTypesTipDelay()
    {
        yield return new WaitForSeconds((GlobalDefVals.PLAYER_RELOAD_TIME + 0.2f) / 2);
        TurnOnFadeImage();
        _ammoTypesTip.SetActive(true);
    }


    private IEnumerator StartTipDelay()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0.1f;
        _moveControlTip.SetActive(true);
        _pauseScript.SetActive(false);
        TurnOnFadeImage();
    }
}
