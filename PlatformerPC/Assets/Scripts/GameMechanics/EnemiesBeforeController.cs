using System.Collections;
using TMPro;
using UnityEngine;

public class EnemiesBeforeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _infoText;

    [SerializeField] private int _numberOfEnemies;

    [SerializeField] private bool _isLastShop;

    private bool _isEnteredFirstTime;


    void Start()
    {
        _infoText.alpha = 0;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody") && !_isEnteredFirstTime)
        {
            if (!_isLastShop)
            {
                _infoText.text = $"{_numberOfEnemies} enemies left until next shop.";
            }
            else
            {
                _infoText.text = $"{_numberOfEnemies} enemies left until finish.";
            }
            LeanTween.value(_infoText.gameObject, a => _infoText.alpha = a, 0f, 1f, 1f); // smoothly fade text. (start fade, end fade, time)
            StartCoroutine(TurnOffDelay());
            _isEnteredFirstTime = true;
        }
    }


    private IEnumerator TurnOffDelay()
    {
        yield return new WaitForSeconds(3f);
        LeanTween.value(_infoText.gameObject, a => _infoText.alpha = a, 1f, 0f, 1.4f); // smoothly fade text. (start fade, end fade, time)
    }
}
