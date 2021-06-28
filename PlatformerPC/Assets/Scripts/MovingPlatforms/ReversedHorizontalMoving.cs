using UnityEngine;

public class ReversedHorizontalMoving : MonoBehaviour
{
    private SliderJoint2D _sliderJoint;


    void Start()
    {
        _sliderJoint = GetComponent<SliderJoint2D>();
    }


    void FixedUpdate()
    {
        if (_sliderJoint.jointSpeed > -0.2f && _sliderJoint.angle == 0f)
        {
            _sliderJoint.angle = 180f;
        }
        else if (_sliderJoint.jointSpeed > -0.2f && _sliderJoint.angle == 180f)
        {
            _sliderJoint.angle = 0f;
        }
    }
}
