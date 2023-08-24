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
            IsRolling = true;
            _currentRollTime = 0;
        }
    }

    private void FixedUpdate()
    {
        if (IsRolling)
        {
            PerformRoll();
        }
    }

    private void PerformRoll()
    {
        _currentRollTime += Time.fixedDeltaTime;

        if (_currentRollTime < _rollingTime || !CanStopRolling)
        {
            float speed = _rollingSpeedCurve.Evaluate(_currentRollTime / _rollingTime) * _rollingSpeed;

            _colliderResizer.Resize = true;
            _canMove.MoveWithCustomSpeed(_rollDirection, speed);

            return;
        }

        StopRoll();
    }

    public void StopRoll()
    {
        _colliderResizer.Resize = false;
        IsRolling = false;
    }
}