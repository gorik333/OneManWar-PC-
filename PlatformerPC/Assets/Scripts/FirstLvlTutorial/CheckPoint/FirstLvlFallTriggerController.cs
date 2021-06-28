using System.Collections;
using UnityEngine;

public class FirstLvlFallTriggerController : MonoBehaviour
{
    private FirstLvlCheckPointsMaster _checkPoints;
    private LoseScreenController _loseScreenController;


    void Start()
    {
        _loseScreenController = FindObjectOfType(typeof(LoseScreenController)) as LoseScreenController;
        _checkPoints = FindObjectOfType(typeof(FirstLvlCheckPointsMaster)) as FirstLvlCheckPointsMaster;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody"))
        {
            if (!FirstLvlTipsController.AllAllow)
            {
                StartCoroutine(RespawnDelay(0.5f));
            }
            else
            {
                _loseScreenController.PlayerDied();
            }
        }
    }


    private IEnumerator RespawnDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        _checkPoints.RespawnOnLastCheckPoint();
    }
}
