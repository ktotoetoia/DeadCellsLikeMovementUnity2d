using UnityEngine;

public class VelocityJump : MovementComponent, ICanJump
{
    [SerializeField] private float _jumpForce;

    public void Jump()
    {
        _slider.ResetNormal();

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    public bool CanJump()
    {
        return _slider.IsOnSurface;
    }
}