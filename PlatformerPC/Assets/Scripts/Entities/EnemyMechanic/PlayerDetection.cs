using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private Transform _headCastPoint;
    [SerializeField] private Transform _legCastPoint;

    private float _maxDistance;

    private EnemyHealthController _enemyHealthController;

    private bool _isDetected;


    void Start()
    {
        _maxDistance = GlobalDefVals.ENEMY_DETECTION_DISTANCE;

        _enemyHealthController = GetComponent<EnemyHealthController>();
    }


    void Update()
    {
        if (_enemyHealthController.IsAlive)
        {
            FindingPlayer();
        }
    }


    private void FindingPlayer()
    {
        Vector2 headEndPos = _headCastPoint.position + Vector3.right * _maxDistance;
        RaycastHit2D headHit = Physics2D.Linecast(_headCastPoint.position, headEndPos, 1 << LayerMask.NameToLayer("Action"));

        Vector2 legEndPos = _legCastPoint.position + Vector3.right * _maxDistance;
        RaycastHit2D legHit = Physics2D.Linecast(_legCastPoint.position, legEndPos, 1 << LayerMask.NameToLayer("Action"));

        HeadHitCheck(headHit, legHit);
    }


    private void HeadHitCheck(RaycastHit2D headHit, RaycastHit2D legHit)
    {
        if (headHit.collider != null || legHit.collider != null)
        {
            if (headHit.collider != null)
            {
                if (headHit.collider.CompareTag("PlayerCollider") || headHit.collider.CompareTag("EnergyShield"))
                {
                    _isDetected = true;
                }
            }
            if (legHit.collider != null)
            {
                if (legHit.collider.CompareTag("PlayerCollider") || legHit.collider.CompareTag("EnergyShield"))
                {
                    _isDetected = true;
                }
            }
        }
        else
        {
            _isDetected = false;
        }
    }


    public bool IsDetected { get => _isDetected; set => _isDetected = value; }

    public float MaxDistance { get => _maxDistance; set => _maxDistance = value; }
}
