using UnityEngine;

public class EdgePointController : MonoBehaviour
{
    private EnemyMovement _enemyMovement;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name.Equals("Body") && collider.CompareTag("EnemyCollider")  )
        {
            _enemyMovement = collider.GetComponentInParent<EnemyMovement>();
            _enemyMovement.EnemyRigidbody.mass += 50;
            _enemyMovement.PlatformEdgeEnter();
        }

        //if (collider.CompareTag("JumpPlatform"))
        //{
        //    Debug.Log(collider.name);
        //}
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name.Equals("Body") && collider.CompareTag("EnemyCollider"))
        {
            _enemyMovement.EnemyRigidbody.mass -= 50;
            _enemyMovement.PlatformEdgeExit();
        }
    }
}
