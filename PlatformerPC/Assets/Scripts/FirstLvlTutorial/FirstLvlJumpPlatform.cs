using UnityEngine;

public class FirstLvlJumpPlatform : MonoBehaviour
{
    private FirstLvlPlayerMovement _firstLvlPlayerMovement;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody"))
        {
            _firstLvlPlayerMovement = collider.GetComponentInParent<FirstLvlPlayerMovement>();
            _firstLvlPlayerMovement.JumpForce = GlobalDefVals.JUMP_PLATFORM_FORCE;
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody"))
        {
            _firstLvlPlayerMovement.JumpForce = GlobalDefVals.JUMP_PLATFORM_FORCE;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCollider") && collider.name.Equals("PlayerBody"))
        {
            _firstLvlPlayerMovement.JumpForce = GlobalDefVals.PLAYER_JUMP_FORCE;
        }
    }
}
