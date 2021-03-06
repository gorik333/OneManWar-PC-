using UnityEngine;

public class FirstLvlPlayerCommonBulletMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _impactParticles;
    [SerializeField] private GameObject _headShotEffect;

    private FirstLvlTipsController _firstLvlTipsController;

    private float _damage;


    void Start()
    {
        _firstLvlTipsController = FindObjectOfType(typeof(FirstLvlTipsController)) as FirstLvlTipsController;
        Destroy(gameObject, GlobalDefVals.COMMON_BULLET_LIFETIME);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyCollider"))
        {
            var enemyHealthController = collider.GetComponentInParent<EnemyHealthController>();
            enemyHealthController.TakeDamage(_damage);

            TotalGameStats.DamageDealt += _damage;
            AudioController.Instance.PlayPlayerCommonBulletBodyHit();

            if (!enemyHealthController.IsAlive)
            {
                _firstLvlTipsController.TurnBalanceTipOn();
            }

            SpawnImpactParticles();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("EnemyHeadCollider"))
        {
            var enemyHealthController = collider.GetComponentInParent<EnemyHealthController>();
            enemyHealthController.TakeDamage(_damage * 3);

            TotalGameStats.DamageDealt += (_damage * 3);
            AudioController.Instance.PlayPlayerCommonBulletHeadHit();

            if (!enemyHealthController.IsAlive)
            {
                _firstLvlTipsController.TurnBalanceTipOn();
            }

            SpawnImpactParticles();
            SpawnHeadShotEffect();
            Destroy(gameObject);
        }
    }


    private void SpawnImpactParticles()
    {
        Instantiate(_impactParticles, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }


    private void SpawnHeadShotEffect()
    {
        Quaternion spread;
        spread = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-GlobalDefVals.HEADSHOT_SPAWN_SPREAD, GlobalDefVals.HEADSHOT_SPAWN_SPREAD)));

        if (spread != null)
        {
            GameObject currentHeadShotEffect = Instantiate(_headShotEffect, new Vector2(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(0.9f, 1.1f)), spread); ; // ТУТ ВСЕ РАБОТАЕТ, ТОЛЬКО НАСТРОИТЬ ОСТАЛОСЬ

            currentHeadShotEffect.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 5);
            LeanTween.alpha(currentHeadShotEffect, 0, 0.2f).setDelay(0.1f);
        }
    }


    public float Damage { get => _damage; set => _damage = value; }
}
