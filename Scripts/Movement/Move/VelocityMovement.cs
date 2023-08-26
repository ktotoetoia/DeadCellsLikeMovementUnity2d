using UnityEngine;

public class SurfaceMovement : MovementComponent, ICanMove
{
    [SerializeField] private float _speed = 5;

    public void Move(float direction)
    {
        MoveWithCustomSpeed(direction, _speed);
    }

    public void MoveWithCustomSpeed(float direction, float speed)
    {
        if (_slider.IsOnSurface)
        {
            SurfaceMove(direction, speed);

            return;
        }

        NonSurfaceMove(direction * speed);
    }

    private void SurfaceMove(float direction, float speed)
    {
        _rigidbody.velocity = _slider.Product(new Vector2(direction, 0)).normalized * speed;
    }

    private void NonSurfaceMove(float direction)
    {
        _rigidbody.velocity = new Vector2(direction, _rigidbody.velocity.y);
    }
}