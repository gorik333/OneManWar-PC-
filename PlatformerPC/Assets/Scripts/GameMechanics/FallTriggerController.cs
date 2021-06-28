using UnityEngine;

public class FallTriggerController : MonoBehaviour
{
    private LoseScreenController _loseScreenController;

    private bool _isFallen;


    void Start()
    {
        _loseScreenController = FindObjectOfType(typeof(LoseScreenController)) as LoseScreenController;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerCollider") && !_isFallen)
        {
            _loseScreenController.PlayerDied();
            _isFallen = true;
        }
    }
}
