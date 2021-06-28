using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FirstLvlPlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Transform _groundColliderTransform;
    [SerializeField] private LayerMask _groundMask; // чтобы получить слой с инспектора

    private Rigidbody2D _playerRigidbody;

    private float _jumpForce;
    private float _speed;
    private float _jumpOffset;

    private bool _isGrounded;


    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        DefaultValues();
    }


    void Update()
    {
        GroundCheck();
    }


    private void DefaultValues()
    {
        _speed = GlobalDefVals.PLAYER_MAX_MOVE_SPEED;
        _jumpForce = GlobalDefVals.PLAYER_JUMP_FORCE;
        _jumpOffset = GlobalDefVals.JUMP_OFFSET;
    }


    private void GroundCheck()
    {
        Vector3 overLapCirclePosition = _groundColliderTransform.position;
        _isGrounded = Physics2D.OverlapCircle(overLapCirclePosition, _jumpOffset, _groundMask); // Крутой способ проверять объект на земле или нет
    }


    public void Jump(bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed && _isGrounded && !WaterCheck.IsOnWater)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
        }
    }


    public void HorizontalMovement(float direction)
    {
        if (Mathf.Abs(direction) > 0.01f)
        {
            _playerRigidbody.velocity = new Vector2(_curve.Evaluate(direction) * _speed, _playerRigidbody.velocity.y);
        }
    }


    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }

    public float Speed { get => _speed; set => _speed = value; }

    public AnimationCurve Curve { get => _curve; set => _curve = value; }

    public float JumpForce { get => _jumpForce; set => _jumpForce = value; }
}
