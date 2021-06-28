using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private PlayerMovement _playerMovement;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            _playerMovement = collider.GetComponentInParent<PlayerMovement>();
            _playerMovement.JumpForce = GlobalDefVals.JUMP_PLATFORM_FORCE;
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            _playerMovement.JumpForce = GlobalDefVals.JUMP_PLATFORM_FORCE;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider"))
        {
            _playerMovement.JumpForce = GlobalDefVals.PLAYER_JUMP_FORCE;
        }
    }
}
