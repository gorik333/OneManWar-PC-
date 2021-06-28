using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    public void StartJumping()
    {
        transform.LeanMoveLocal(new Vector2(650, 150), 1).setEaseOutQuart().setLoopPingPong();
    }
}
