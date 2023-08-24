using UnityEngine;

public class VelocityJump : MovementComponent, ICanJump
{
    [SerializeField] private float _jumpForce;

    public void Jump()
    {
        if (_slider.IsOnSurface)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }
}