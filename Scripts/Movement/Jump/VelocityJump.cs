using UnityEngine;

public class VelocityJump : MovementComponent, ICanJump
{
    public event System.Action OnJumpPerformed;

    [SerializeField] private float _jumpForce;

    public bool TryJump()
    {
        bool jumped = CanJump();

        if (jumped)
        {
            Jump();
        }

        return jumped;
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        OnJumpPerformed?.Invoke();
    }

    public bool CanJump()
    {
        return _slider.IsOnSurface;
    }
}