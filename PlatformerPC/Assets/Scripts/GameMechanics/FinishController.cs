using UnityEngine;

public class FinishController : MonoBehaviour
{
    private WinScreenController _winScreenController;

    private bool _isFinished;


    void Awake()
    {
        _winScreenController = FindObjectOfType(typeof(WinScreenController)) as WinScreenController;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && !_isFinished)
        {
            _isFinished = true;

            SaveDataController.SaveCompletedLevelData();

            _winScreenController.PlayerFinished();
        }
    }
}
