using UnityEngine;

public class FirstLvlCheckPoint : MonoBehaviour
{
    private FirstLvlCheckPointsMaster _checkPoints;


    void Start()
    {
        _checkPoints = GameObject.FindGameObjectWithTag("CPM").GetComponent<FirstLvlCheckPointsMaster>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            AudioController.Instance.PlayCheckPointSound();

            _checkPoints.LastCheckPointPos = transform.position;
            _checkPoints.SaveCurrentData();
            gameObject.SetActive(false);
        }
    }
}
