using UnityEngine;
using System.Collections;

public class EnemyBulletMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _impactParticles;

    private float _damage;

    private bool _isDamageDealt;


    void Start()
    {
        Destroy(gameObject, GlobalDefVals.COMMON_BULLET_LIFETIME);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnergyShield") && !_isDamageDealt)
        {
            _isDamageDealt = true;

            collider.GetComponentInParent<PlayerHealthController>().TakeDamage(_damage, true);
            _damage = 0;
            SpawnImpactParticles();

            AudioController.Instance.PlayEnemyCommonBulletShieldHit();

            Destroy(gameObject);
        }
        if (collider.CompareTag("PlayerCollider") && !_isDamageDealt)
        {
            _isDamageDealt = true;
            StartCoroutine(DelayBeforeDamageIfShieldActivated(collider));
        }
    }


    private void SpawnImpactParticles()
    {
        Instantiate(_impactParticles, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }


    private IEnumerator DelayBeforeDamageIfShieldActivated(Collider2D collider) // I did it because object destroys in the end of frame
    {
        yield return new WaitForEndOfFrame();

        _isDamageDealt = false;

        if (_damage != 0)
        {
            AudioController.Instance.PlayEnemyCommonBulletBodyHit();
        }

        collider.GetComponentInParent<PlayerHealthController>().TakeDamage(_damage, false);
        _damage = 0;
        SpawnImpactParticles();
        Destroy(gameObject);
    }


    public float Damage { get => _damage; set => _damage = value; }
}
