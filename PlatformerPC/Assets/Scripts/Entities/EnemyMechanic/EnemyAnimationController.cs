using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _enemyShootAnimation;

    private EnemyAudioController _enemyAudioController;

    private Animator _enemyAnimator;

    private Rigidbody2D _enemyRigidbody;

    private float _moveSpeedToRunAnim;


    void Start()
    {
        _enemyAudioController = GetComponentInChildren<EnemyAudioController>();

        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();

        _enemyAnimator.SetBool("isGrounded", true);
        _moveSpeedToRunAnim = GlobalDefVals.MOVE_SPEED_TO_RUN_ANIM;
    }


    void Update()
    {
        StartRunAnimation();
    }


    public void StartCrouchAnimation(bool flag)
    {
        _enemyAnimator.SetBool("isCrouching", flag);
    }


    public void StartJumpAnimation(bool state)
    {
        _enemyAnimator.SetBool("isGrounded", state);
    }


    private void StartRunAnimation()
    {
        if (_enemyRigidbody.velocity.x > _moveSpeedToRunAnim || _enemyRigidbody.velocity.x < -_moveSpeedToRunAnim)
        {
            _enemyAnimator.SetBool("isRunning", true);

            _enemyAudioController.IsRunning = true;
        }
        else
        {
            _enemyAnimator.SetBool("isRunning", false);

            _enemyAudioController.IsRunning = false;
        }
    }


    public void StartShootAnim()
    {
        _enemyShootAnimation.Play("CommonShooting", 0, Random.Range(0.0f, 1.0f));
    }
}
