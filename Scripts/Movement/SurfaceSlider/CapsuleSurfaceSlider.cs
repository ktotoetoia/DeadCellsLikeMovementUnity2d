using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CapsuleSurfaceSlider : MonoBehaviour, ISurfaceSlider
{
    public event System.Action<Vector2> OnNormalChanged;

    [SerializeField] private float _maxNormalX = 0.8f;
    [SerializeField] private float _castDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer = 1 << 6;
    [SerializeField] private float _boundsNormalDetectionOffset;
    private CapsuleCollider2D _collider;
    private Vector2 _normal;

    public Vector2 Normal
    {
        get
        {
            return _normal;
        }
        set
        {
            if(_normal != value)
            {
                _normal = value;

                OnNormalChanged?.Invoke(_normal);
            }
        }
    }

    public bool IsOnSurface { get { return Normal != default; } }
    public List<ContactPoint2D> AllContacts { get; private set; } = new List<ContactPoint2D>();

    private void Start()
    {
        TryGetComponent(out _collider);
    }

    private void FixedUpdate()
    {
        OnCollisionChanged();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionChanged();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionChanged();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionChanged();
    }

    private void OnCollisionChanged()
    {
        _collider.GetContacts(AllContacts);
        UpdateNormal();
    }

    private void UpdateNormal()
    {
        float radius = _collider.size.x/2;
        Vector2 origin = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y + radius);

        if (IsOnSurface)
        {
            IEnumerable<RaycastHit2D> hits = Physics2D.CircleCastAll(origin, radius, Vector2.down, _castDistance, _groundLayer)
                .Where(x => x.point.y < origin.y);

            Normal = GetBestNormal( hits.Select(x => x.normal));

            return;
        }

        Normal = GetBestNormal(AllContacts.Where(x => 
        x.point.x >_collider.bounds.min.x + _boundsNormalDetectionOffset &&
        x.point.x < _collider.bounds.max.x - _boundsNormalDetectionOffset).Select(x => x.normal));
    }

    private Vector2 GetBestNormal(IEnumerable<Vector2> normals)
    {
        return normals.Where(normal => IsValidNormal(normal))
            .OrderByDescending(normal => Mathf.Abs(normal.x))
            .FirstOrDefault();
    }

    public Vector2 Product(Vector2 direction)
    {
        return direction - Vector2.Dot(direction, Normal) * Normal;
    }

    public bool IsValidNormal(Vector2 normal)
    {
        return normal.y > 0 && Mathf.Abs(normal.x) < _maxNormalX;
    }

    public void ResetNormal()
    {
        Normal = default;
    }
}