using UnityEngine;

public class FirstLvlPlayerStrengthenedBulletMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _impactParticles;
    [SerializeField] private GameObject _headShotEffect;

    private FirstLvlTipsController _firstLvlTipsController;

    private const int MAX_ENEMIES_HURT = 3;

    private float _damage;

    private bool _isRightDirection;

    private int _enemiesHurt;


    void Start()
    {
        _firstLvlTipsController =  FindObjectOfType(typeof(FirstLvlTipsController)) as FirstLvlTipsController;
        Destroy(gameObject, GlobalDefVals.STRENGTHENED_BULLET_LIFETIME);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyCollider"))
        {
            collider.GetComponentInParent<EnemyHealthController>().TakeDamage(_damage);
            SpawnImpactParticles();

            TotalGameStats.DamageDealt += _damage;

            if (!collider.GetComponentInParent<EnemyHealthController>().IsAlive)
            {
                _firstLvlTipsController.TurnBalanceTipOn();
            }

            _enemiesHurt++;
        }
        else if (collider.CompareTag("EnemyHeadCollider"))
        {
            collider.GetComponentInParent<EnemyHealthController>().TakeDamage(_damage * 3);
            SpawnImpactParticles();
            SpawnHeadShotEffect();

            TotalGameStats.DamageDealt += (_damage * 3);

            if (!collider.GetComponentInParent<EnemyHealthController>().IsAlive)
            {
                _firstLvlTipsController.TurnBalanceTipOn();
            }

            _enemiesHurt++;
        }

        if (_enemiesHurt >= MAX_ENEMIES_HURT)
        {
            _damage = 0;
            Destroy(gameObject);
        }
    }


    private void SpawnImpactParticles()
    {
        if (_isRightDirection)
        {
            Instantiate(_impactParticles, new Vector2(transform.position.x, transform.position.y), _impactParticles.transform.rotation);
        }
        else
        {
            Instantiate(_impactParticles, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }

        AudioController.Instance.PlayPlayerStrengthenedBulletHit();
    }


    private void SpawnHeadShotEffect()
    {
        Quaternion spread;
        spread = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-GlobalDefVals.HEADSHOT_SPAWN_SPREAD, GlobalDefVals.HEADSHOT_SPAWN_SPREAD)));

        if (spread != null)
        {
            GameObject currentHeadShotEffect = Instantiate(_headShotEffect, new Vector2(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(0.9f, 1.1f)), spread); // ТУТ ВСЕ РАБОТАЕТ, ТОЛЬКО НАСТРОИТЬ ОСТАЛОСЬ

            currentHeadShotEffect.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 5);
            LeanTween.alpha(currentHeadShotEffect, 0, 0.2f).setDelay(0.1f);
        }
    }


    public float Damage { get => _damage; set => _damage = value; }

    public bool IsRightDirection { get => _isRightDirection; set => _isRightDirection = value; }
}
