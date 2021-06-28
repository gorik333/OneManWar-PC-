using UnityEngine;

public class LaserImpactEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.21f);
        gameObject.LeanScale(Vector3.zero, 0.18f).setDelay(0.03f);
    }
}
