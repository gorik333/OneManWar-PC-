using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Transform prefab;

    private PointEffector2D pointEffector;

    public bool explode;


    void Start()
    {
        pointEffector = GetComponent<PointEffector2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            explode = true;
        }
    }


    void FixedUpdate()
    {
        if (explode)
        {
            StartExplosion();
        }
    }


    private void StartExplosion()
    {
        pointEffector.enabled = true;
        Instantiate(prefab, transform.position, Quaternion.identity);
        StartCoroutine(DestroyDelay());
        explode = false;
    }

    
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
