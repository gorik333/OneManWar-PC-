using System.Collections;
using UnityEngine;

public class HeadShotController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyDelay());
    }


    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
