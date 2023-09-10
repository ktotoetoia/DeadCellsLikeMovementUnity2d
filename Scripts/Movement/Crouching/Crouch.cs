using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleColliderResizer))]
public class Crouch : MovementComponent, ICanCrouch
{
    [SerializeField] private float _fallSpeed;
    private CapsuleColliderResizer _colliderResizer;
    private bool _isCoroutineRunning;

    public bool IsCrouching { get; set; }
    public bool IsFalling { get; set; }

    protected override void Start()
    {
        base.Start();

        _colliderResizer = GetComponent<CapsuleColliderResizer>();
    }

    private void FixedUpdate()
    {
        HandleCrouching();
    }

    private void HandleCrouching()
    {
        if (IsCrouching && !_isCoroutineRunning)
        {
            StartCoroutine(CrouchingCoroutine());
        }
    }

    private IEnumerator CrouchingCoroutine()
    {
        _isCoroutineRunning = true;
        _colliderResizer.Resize = true;

        yield return WhileCrouching();

        _colliderResizer.Resize = false;
        _isCoroutineRunning = false;
    }

    protected IEnumerator WhileCrouching()
    {
        _rigidbody.velocity = new Vector2(0, -_fallSpeed);

        while (IsCrouching || IsFalling)
        {
            if (_slider.IsOnSurface)
            {
                _rigidbody.velocity = new Vector2(0, 0);
            }

            IsFalling = !_slider.IsOnSurface;

            yield return null;
        }

        IsFalling = false;
    }
}