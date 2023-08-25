using UnityEngine;

public class CapsuleColliderResizer : MonoBehaviour
{
    [SerializeField] private float _crouchingMultiplier = 0.5f;
    [SerializeField] private LayerMask _groundLayer = 1 << 6;
    private CapsuleCollider2D _collider;
    private Vector2 _colliderMin;
    private Vector2 _defaultSize;
    private Vector2 _modifiedSize;
    private Vector2 _defaultOffset;
    private Vector2 _modifiedOffset;
    private bool resize;

    public bool CanResizeToNormal { get; private set; }
    public bool Resize
    {
        get
        {
            return resize;
        }
        set
        {
            resize = value;
         
            if (resize)
            {
                _collider.size = _modifiedSize;
                _collider.offset = _modifiedOffset;
            }
            else if (CanResizeToNormal)
            {
                _collider.size = _defaultSize;
                _collider.offset = _defaultOffset;
            }
        }
    }

    protected void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _colliderMin = _collider.bounds.min - (Vector3)_collider.offset - transform.position;
        _defaultSize = _collider.size;
        _defaultOffset = _collider.offset;
        _modifiedSize = new Vector2(_defaultSize.x, _defaultSize.y * _crouchingMultiplier);
        _modifiedOffset = _defaultOffset + new Vector2(0, -(_colliderMin.y * _crouchingMultiplier - _colliderMin.y));
    }

    private void FixedUpdate()
    {
        CanResizeToNormal = !Physics2D.OverlapCapsule(transform.position + (Vector3)_defaultOffset, _defaultSize * 0.95f, _collider.direction, 0, _groundLayer);
    }
}