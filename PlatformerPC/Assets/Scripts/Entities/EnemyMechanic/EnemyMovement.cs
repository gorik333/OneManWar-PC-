using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _groundMask; // чтобы получить слой с инспектора
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _enemyBody;
    [SerializeField] private Transform _groundColliderTransform;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;

    private Rigidbody2D _enemyRigidbody;
    private SpriteRenderer _enemySprite;

    private EnemyHealthController _enemyHealthController;
    private EnemyAnimationController _enemyAnimationController;
    private PlayerDetection _playerDetection;
    private EnemyShootMechanic _enemyShootMechanic;

    private float _speed;
    private float _timeToRevert;

    private float _currentState;
    private float _currentTimeToRevertModel;
    private float _currentTimeToRotate;
    private float _distanceToPlayer;

    private bool _isGrounded;
    private bool _isOnThePlatformEdge;
    private bool _ignoreNextEnemyStopper;
    private bool _isFirstTime;


    void Awake()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponent<SpriteRenderer>();

        _enemyShootMechanic = GetComponent<EnemyShootMechanic>();
        _playerDetection = GetComponent<PlayerDetection>();
        _enemyHealthController = GetComponent<EnemyHealthController>();
        _enemyAnimationController = GetComponent<EnemyAnimationController>();

        DefaultValues();
    }


    private void DefaultValues()
    {
        _speed = GlobalDefVals.ENEMY_MOVE_SPEED;
        _timeToRevert = GlobalDefVals.ENEMY_TIME_TO_REVERT;
        _currentState = WALK_STATE;
        _currentTimeToRevertModel = 0;
        _distanceToPlayer = Random.Range(7f, 11f);
    }


    void FixedUpdate()
    {
        if (_enemyHealthController.IsAlive && _isGrounded && !DefaultLevelController.IsGameEnd)
        {
            if (!_playerDetection.IsDetected)
            {
                SwitchStates();
            }
            else
            {
                AttackPlayer();
            }
        }
        GroundCheck();
    }


    private void GroundCheck()
    {
        Vector3 overLapCirclePosition = _groundColliderTransform.position;
        _isGrounded = Physics2D.OverlapCircle(overLapCirclePosition, GlobalDefVals.JUMP_OFFSET, _groundMask); // Крутой способ проверять объект на земле или нет
        _enemyAnimationController.StartJumpAnimation(_isGrounded);
    }


    private void SwitchStates()
    {
        if (_currentTimeToRevertModel >= _timeToRevert && _currentState == IDLE_STATE)
        {
            _enemyAnimationController.StartCrouchAnimation(false);
            _currentTimeToRevertModel = 0;
            _currentTimeToRotate = 0;
            _currentState = REVERT_STATE;
        }

        switch (_currentState)
        {
            case IDLE_STATE:
                PatrolIdleState();
                break;
            case WALK_STATE:
                PatrolWalkState();
                break;
            case REVERT_STATE:
                PatrolRevertState();
                break;
        }
    }


    private void PatrolIdleState()
    {
        _currentTimeToRevertModel += Time.deltaTime;
        _enemyAnimationController.StartCrouchAnimation(true);
        CrouchRotation();
    }


    private void PatrolRevertState()
    {
        Rotate();
        _currentState = WALK_STATE;
    }


    private void PatrolWalkState()
    {
        if (_enemyHealthController.IsAlive)
        {
            _enemyRigidbody.velocity = Vector2.right * _speed;
        }
    }


    private void AttackPlayer()
    {
        if (_player != null)
        {
            if (Vector2.Distance(transform.position, _player.position) > _distanceToPlayer && !_isOnThePlatformEdge)
            {
                _enemyAnimationController.StartCrouchAnimation(false);
                _enemyRigidbody.velocity = new Vector2(_speed * GlobalDefVals.ENEMY_DETECT_BOOST_MULTIPLIER, 0);
            }
            _enemyShootMechanic.Shoot();
        }
    }


    private void CrouchRotation()
    {
        _currentTimeToRotate += Time.deltaTime;

        if (_currentTimeToRotate >= GlobalDefVals.ENEMY_TIME_TO_REVERT / 3)
        {
            Rotate();
            _currentTimeToRotate = 0f;
        }
    }


    public void RotateToAttack()
    {
        if (!_playerDetection.IsDetected && !_isFirstTime)
        {
            if (_enemyBody.rotation.y != 0)
            {
                _enemyBody.rotation = Quaternion.Euler(0, 0, 0);
                _enemySprite.flipX = false;
            }
            else
            {
                _enemyBody.rotation = Quaternion.Euler(0, 180, 0);
                _enemySprite.flipX = true;
            }
            _playerDetection.MaxDistance *= -1;
            _speed *= -1;

            StartCoroutine(FirstTimeDelay());
            _currentTimeToRevertModel = 0f;
            _currentTimeToRotate = 0;
        }
    }


    private void Rotate()
    {
        if (_enemyBody.rotation.y == 0)
        {
            _enemyBody.rotation = Quaternion.Euler(0, 180, 0);
            _enemySprite.flipX = true;
        }
        else
        {
            _enemyBody.rotation = Quaternion.Euler(0, 0, 0);
            _enemySprite.flipX = false;
        }
        _playerDetection.MaxDistance *= -1;
        _speed *= -1;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyStopper") && !_ignoreNextEnemyStopper)
        {
            _currentState = IDLE_STATE;
        }
        else if (collider.CompareTag("EnemyStopper") && _ignoreNextEnemyStopper)
        {
            StartCoroutine(UnignoreDelay());
        }
    }


    public void PlatformEdgeEnter()
    {
        _isOnThePlatformEdge = true;
        _ignoreNextEnemyStopper = true;
        _currentState = IDLE_STATE;
        _currentTimeToRevertModel = 0f;
        _currentTimeToRotate = 0;
    }


    public void PlatformEdgeExit()
    {
        _isOnThePlatformEdge = false;
    }


    private IEnumerator FirstTimeDelay()
    {
        _isFirstTime = true;
        yield return new WaitForSeconds(0.2f);
        _isFirstTime = false;
    }


    private IEnumerator UnignoreDelay() // because entity has three colliders
    {
        yield return new WaitForSeconds(1.5f);
        _ignoreNextEnemyStopper = false;
    }


    public Rigidbody2D EnemyRigidbody { get => _enemyRigidbody; set => _enemyRigidbody = value; }
}
