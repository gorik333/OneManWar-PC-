using System.Collections;
using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    private static bool isOnWater;

    private Rigidbody2D playerRigidbody;

    private PlayerMovement playerMovement;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            UnderWaterSetUp();
            isOnWater = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            DefaultValues();
            isOnWater = false;
        }
    }


    private void UnderWaterSetUp()
    {
        playerMovement.Speed = 1f;
        playerRigidbody.drag = 3f;
        float verticalDirection = Input.GetAxis(GlobalStringVars.VERTICAL_AXIS);
        playerRigidbody.velocity = new Vector2(playerMovement.Curve.Evaluate(playerRigidbody.velocity.x), verticalDirection);
    }


    private void DefaultValues()
    {
        playerRigidbody.drag = 0f;
        playerMovement.Speed = 3f;
    }


    public static bool IsOnWater { get => isOnWater; set => isOnWater = value; }
}
