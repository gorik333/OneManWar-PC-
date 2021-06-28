using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _parallaxEffect;

    private float _length;
    private float _startPos;


    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void FixedUpdate()
    {
        float temp = _camera.transform.position.x * (1 - _parallaxEffect);
        float dist = _camera.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if(temp > _startPos + _length - 5)
        {
            _startPos += _length;
        }
        else if(temp < _startPos - _length + 5)
        {
            _startPos -= _length;
        }
    }
}
