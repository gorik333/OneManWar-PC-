using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerShootAnimation;
    [SerializeField] private Animator _playerShieldAnimation;

    private Animator _playerAnimation;
    private Rigidbody2D _playerRigidbody;

    private PlayerMovement _playerMovement;
    private PlayerShootMechanic _playerShootMechanic;
    private AudioController _audioController;

    private float _moveSpeedToRunAnim;


    void Start()
    {
        _playerShootMechanic = GetComponent<PlayerShootMechanic>();
        _playerMovement = GetComponent<PlayerMovement>();
        _audioController = AudioController.Instance;

        _playerAnimation = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();

        _moveSpeedToRunAnim = GlobalDefVals.MOVE_SPEED_TO_RUN_ANIM;
    }


    void FixedUpdate()
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
            _audioController.IsRunning = false;
            _playerAnimation.SetBool("isRunning", false);
        }
    }


    public void StartShieldAnimation(bool state)
    {
        _playerShieldAnimation.SetBool("isActivated", state);
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


    public float MoveSpeedToRunAnim { get => _moveSpeedToRunAnim; set => _moveSpeedToRunAnim = value; }
}