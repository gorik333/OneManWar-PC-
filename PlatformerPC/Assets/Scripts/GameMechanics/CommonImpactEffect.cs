using System.Collections;
using UnityEngine;

public class CommonImpactEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
