using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class FirstLvlPlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerShootAnimation;
    [SerializeField] private Animator _playerShieldAnimation;

    private Animator _playerAnimation;
    private Rigidbody2D _playerRigidbody;

    private FirstLvlPlayerMovement _playerMovement;
    private FirstLvlPlayerShootMechanic _playerShootMechanic;

    private AudioController _audioController;

    private float _moveSpeedToRunAnim;


    void Start()
    {
        _playerShootMechanic = GetComponent<FirstLvlPlayerShootMechanic>();
        _playerMovement = GetComponent<FirstLvlPlayerMovement>();
        _audioController = AudioController.Instance;

        _playerAnimation = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();

        _moveSpeedToRunAnim = GlobalDefVals.MOVE_SPEED_TO_RUN_ANIM;
    }


    void LateUpdate()
    {
        StartRunningAnim();
        StartJumpAnim();
    }


    private void StartRunningAnim()
    {
        if ((_playerRigidbody.velocity.x > _moveSpeedToRunAnim || _playerRigidbody.velocity.x < -_moveSpeedToRunAnim) && _playerMovement.IsGrounded)
        {
            _playerAnimation.SetBool("isRunning", true);

            _audioController.IsRunning = true;
            _audioController.CurrentPlayerMoveSpeed = Mathf.Abs(_playerRigidbody.velocity.x);
        }
        else
        {
            _playerAnimation.SetBool("isRunning", false);
            _audioController.IsRunning = false;
        }
    }


    public void StartShieldAnimation(bool state)
    {
        _playerShieldAnimation.SetBool("isActivated", state);
    }


    public void Respawn()
    {
        _playerAnimation.SetBool("isDead", false);
        _playerAnimation.SetTrigger("Respawn");
    }


    private void StartJumpAnim()
    {
        _playerAnimation.SetBool("isGrounded", _playerMovement.IsGrounded);
    }


    public void StartSitAnim(bool state, bool isJumpButtonPressed)
    {
        if (state && _playerMovement.IsGrounded && !isJumpButtonPressed)
        {
            _playerShootMechanic.MaxBulletSpreadAngle = 0.75f;
            _playerRigidbody.velocity = new Vector2(0, _playerRigidbody.velocity.y);
            _playerAnimation.SetBool("isCrouching", true);
        }
        else
        {
            _playerShootMechanic.MaxBulletSpreadAngle = 2f;
            _playerAnimation.SetBool("isCrouching", false);
        }
    }


    public void StartCommonShootAnim()
    {
        _playerShootAnimation.Play("CommonShooting", 0, Random.Range(0.0f, 1.0f));
    }

    
    public void StartStrengthenedShootAnim()
    {
        _playerShootAnimation.Play("StrengthenedShooting", 0, Random.Range(0.0f, 1.0f));
    }


    public void StartDeathAnim()
    {
        _playerAnimation.SetBool("isDead", true);
    }
}