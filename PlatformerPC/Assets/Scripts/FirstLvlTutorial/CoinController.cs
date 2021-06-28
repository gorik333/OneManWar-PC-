using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            Destroy(gameObject);
        }
    }
}
