using System.Linq;
using UnityEngine;

public class Roller : MovementComponent, ICanRoll
{
    [SerializeField] private AnimationCurve _rollingSpeedCurve;
    [SerializeField] private float _rollingTime;
    [SerializeField] private float _rollingSpeed;
    private CapsuleColliderResizer _colliderResizer;
    private ICanMove _canMove;
    private float _currentRollTime;
    private float _rollDirection;
    private bool _firstFrame;

    public bool IsRolling { get; set; }
    public bool CanStopRolling { get { return _colliderResizer.CanResizeToNormal; } }

    protected override void Start()
    {
        base.Start();

        _colliderResizer = GetComponent<CapsuleColliderResizer>();
        _canMove = GetComponent<ICanMove>();
    }

    public void Roll(float direction)
    {
        if (!IsRolling)
        {
            _rollDirection = direction;
            _currentRollTime = 0;
            IsRolling = true;
            _firstFrame = true;
        }
    }

    private void FixedUpdate()
    {
        if (IsRolling)
        {
            if (_firstFrame)
            {
                _colliderResizer.Resize = true;
                _firstFrame = false;

                return;
            }

            PerformRoll();
        }
    }

    private void PerformRoll()
    {
        _currentRollTime += Time.fixedDeltaTime;

        if (_currentRollTime < _rollingTime || !CanStopRolling)
        {
            UpdateDirection();

            float speed = _rollingSpeedCurve.Evaluate(_currentRollTime / _rollingTime) * _rollingSpeed;

            _canMove.MoveWithCustomSpeed(_rollDirection, speed);

            return;
        }

        StopRoll();
    }

    private void UpdateDirection()
    {
        Vector2 normal = _slider.AllContacts.Select(x => x.Normal).FirstOrDefault(x => !_slider.IsValidNormal(x));

        if (normal != default && !_colliderResizer.CanResizeToNormal)
        {
            _rollDirection = normal.x.CompareTo(0);
        }
    }

    public void StopRoll()
    {
        _colliderResizer.Resize = false;
        IsRolling = false;
    }
}