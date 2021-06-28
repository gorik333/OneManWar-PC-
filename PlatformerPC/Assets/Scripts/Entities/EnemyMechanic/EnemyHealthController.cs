using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _healthBarBackground;

    private EnemyMovement _enemyMovement;

    private bool _isAlive = true;

    private float _currentHealth;
    private float _maxHealth;


    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        DefautltValues();
    }


    private void DefautltValues()
    {
        _maxHealth = GlobalDefVals.MAX_HIT_POINTS;
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (_isAlive)
        {
            _enemyMovement.RotateToAttack(); // like if player hit him in back he rotates and attack player

            _currentHealth -= damage;
            _healthBar.color = new Color(1f - _currentHealth / _maxHealth, _currentHealth / _maxHealth, 0); // transition from green to red
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        CheckIfAlive();
    }


    private void CheckIfAlive()
    {
        if (_currentHealth <= 0 && _isAlive)
        {
            GetComponent<Animator>().SetBool("isDeath", true);
            _healthBar.fillAmount = 0f;
            _healthBarBackground.gameObject.SetActive(false);

            TotalGameStats.EarnSkillPointsFromEnemies();
            DefaultLevelController.EarnMoneyFromEnemy();

            StartCoroutine(DisappearanceDelay());

            _isAlive = false;
        }
    }


    private IEnumerator DisappearanceDelay()
    {
        yield return new WaitForSeconds(GlobalDefVals.DISAPPEARANCE_DELAY);
        Destroy(gameObject);
    }


    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
}
