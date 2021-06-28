using UnityEngine;

[RequireComponent(typeof(FirstLvlPlayerMovement))]
[RequireComponent(typeof(FirstLvlPlayerShootMechanic))]
public class FirstLvlPlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;

    private FirstLvlPlayerMovement _playerMovement;
    private FirstLvlPlayerShootMechanic _playerShootMechanic;
    private FirstLvlPlayerAnimationController _animationController;
    private FirstLvlPlayerHealthController _playerHealthController;

    public static bool _isPopUpDialogActive;

    private bool _isReadyToFire;
    private bool _isReadyToLookAtMouseCursor;
    private bool _isReadyToJump;


    void Awake()
    {
        _playerHealthController = GetComponent<FirstLvlPlayerHealthController>();
        _animationController = GetComponent<FirstLvlPlayerAnimationController>();
        _playerShootMechanic = GetComponent<FirstLvlPlayerShootMechanic>();
        _playerMovement = GetComponent<FirstLvlPlayerMovement>();
    }


    void Update()
    {
        if (!MiniShopController.IsShopping && _playerHealthController.IsAlive && !MiniMenuController.PauseEnabled && !DefaultLevelController.IsGameEnd && Time.timeScale != 0)
        {
            SitJumpAndRunInputs();

            if (_isReadyToFire && !_isPopUpDialogActive)
            {
                ChangeAmmoType();
                FireAndReloadInputs();
            }
            if (_isReadyToLookAtMouseCursor)
            {
                LookAtMouseCursor();
            }
        }
    }


    private void ChangeAmmoType()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerShootMechanic.SwitchAmmoType();
        }
    }


    private void LookAtMouseCursor()
    {
        var targetDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (targetDir.x > 0)
        {
            _playerBody.rotation = Quaternion.Euler(0, 0, 0);
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (targetDir.x < 0)
        {
            _playerBody.rotation = Quaternion.Euler(0, 180, 0);
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    private void SitJumpAndRunInputs()
    {
        bool isSitButtonPressed = Input.GetButton(GlobalStringVars.SIT);
        bool isjumpButtonPressed = Input.GetButton(GlobalStringVars.JUMP);

        _animationController.StartSitAnim(isSitButtonPressed, isjumpButtonPressed);

        if (_isReadyToJump)
        {
            _playerMovement.Jump(isjumpButtonPressed);
        }

        if (!isSitButtonPressed || !_playerMovement.IsGrounded)
        {
            float horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            _playerMovement.HorizontalMovement(horizontalDirection);
        }
    }


    private void FireAndReloadInputs()
    {
        if (Input.GetButton(GlobalStringVars.FIRE_1))
        {
            _playerShootMechanic.Shoot();
        }
        if (!_playerShootMechanic.IsReloading && Input.GetKeyDown(KeyCode.R))
        {
            _playerShootMechanic.Reload();
        }
    }


    public bool IsReadyToFire { get => _isReadyToFire; set => _isReadyToFire = value; }

    public bool IsReadyToLookAtMouseCursor { get => _isReadyToLookAtMouseCursor; set => _isReadyToLookAtMouseCursor = value; }

    public bool IsReadyToJump { get => _isReadyToJump; set => _isReadyToJump = value; }
}
