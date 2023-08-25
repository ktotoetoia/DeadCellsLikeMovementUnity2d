using UnityEngine;

[RequireComponent(typeof(CapsuleSurfaceSlider))]
public class MovementComponent : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected ISurfaceSlider _slider;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _slider = GetComponent<ISurfaceSlider>();
    }
}