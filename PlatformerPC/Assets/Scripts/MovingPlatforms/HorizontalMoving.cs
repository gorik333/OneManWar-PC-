using UnityEngine;

public class HorizontalMoving : MonoBehaviour
{
    [Header ("Change speed to negative too")]
    [SerializeField] private bool _isReversed;

    private SliderJoint2D _sliderJoint;

    public static bool IsOnPlatform;

    private bool _isRightDirection;

    private PlayerMovement _playerMovement;


    void Awake()
    {
        _sliderJoint = GetComponent<SliderJoint2D>();
    }


    void FixedUpdate()
    {
        if (!_isReversed)
        {
            if (_sliderJoint.jointSpeed < 0.2f && _sliderJoint.angle == 0f)
            {
                _isRightDirection = true;

                _sliderJoint.angle = 180f;
            }
            else if (_sliderJoint.jointSpeed < 0.2f && _sliderJoint.angle == 180f)
            {
                _isRightDirection = false;

                _sliderJoint.angle = 0f;
            }
        }
        else
        {
            if (_sliderJoint.jointSpeed > -0.2f && _sliderJoint.angle == 0f)
            {
                _isRightDirection = false;

                _sliderJoint.angle = 180f;
            }
            else if (_sliderJoint.jointSpeed > -0.2f && _sliderJoint.angle == 180f)
            {
                _isRightDirection = true;

                _sliderJoint.angle = 0f;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsOnPlatform)
        {
            IsOnPlatform = true;

            collision.gameObject.GetComponent<PlayerAnimationController>().MoveSpeedToRunAnim = Mathf.Abs(_sliderJoint.motor.motorSpeed) + 0.08f;
            _playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerMovement.IsRightDirection = _isRightDirection;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsOnPlatform)
        {
            IsOnPlatform = false;

            collision.gameObject.GetComponent<PlayerAnimationController>().MoveSpeedToRunAnim = 0.12f;
        }
    }
}
