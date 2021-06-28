using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShootMechanic))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] Transform _playerBody;

    private PlayerMovement _playerMovement;
    private PlayerShootMechanic _playerShootMechanic;
    private PlayerAnimationController _animationController;
    private PlayerHealthController _playerHealthController;


    void Awake()
    {
        _playerHealthController = GetComponent<PlayerHealthController>();
        _animationController = GetComponent<PlayerAnimationController>();
        _playerShootMechanic = GetComponent<PlayerShootMechanic>();
        _playerMovement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (!MiniShopController.IsShopping && _playerHealthController.IsAlive && !MiniMenuController.PauseEnabled 
            && !DefaultLevelController.IsGameEnd && !CutsceneController.IsCutSceneEnabled && Time.timeScale != 0) // Blocks player movement while some actions
        {
            SitJumpAndRunInputs();
            FireAndReloadInputs();
            ChangeAmmoType();
            LookAtMouseCursor();
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


    private void ChangeAmmoType()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerShootMechanic.SwitchAmmoType();
        }
    }


    private void SitJumpAndRunInputs()
    {
        bool isSitButtonPressed = Input.GetButton(GlobalStringVars.SIT);
        bool isjumpButtonPressed = Input.GetButton(GlobalStringVars.JUMP);

        _animationController.StartSitAnim(isSitButtonPressed, isjumpButtonPressed);
        _playerMovement.Jump(isjumpButtonPressed);

        if (!isSitButtonPressed || !_playerMovement.IsGrounded)
        {
            var horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            _playerMovement.HorizontalMovement(horizontalDirection);
        }
    }


    private void FireAndReloadInputs()
    {
        if (Input.GetButton(GlobalStringVars.FIRE_1))
        {
            _playerShootMechanic.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerShootMechanic.Reload();
        }
    }
}
