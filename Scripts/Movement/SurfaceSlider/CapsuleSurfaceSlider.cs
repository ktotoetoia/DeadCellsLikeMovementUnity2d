using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleSurfaceSlider : MonoBehaviour, ISurfaceSlider
{
    public event System.Action OnNormalChanged;

    [Range(0, 180)][SerializeField] private float _maxNormalDegrees = 50;
    [SerializeField] private LayerMask _groundLayer = 1 << 6;
    [SerializeField] private float _onSurfaceCastDistance = 0.1f;
    [SerializeField] private float _boundsNormalDetectionOffset;
    private CapsuleCollider2D _collider;

    public IEnumerable<PointInfo> AllContacts { get; private set; } = new List<PointInfo>();
    public bool IsOnSurface { get { return AllContacts.Any(x => IsValidNormal(x.Normal)); } }

    private void Start()
    {
        TryGetComponent(out _collider);
    }

    private void FixedUpdate()
    {
        UpdateNormals();
    }

    private void UpdateNormals()
    {
        if (IsOnSurface)
        {
            UpdateOnSurface();
        }
        else
        {
            UpdateNotOnSurface();
        }

        OnNormalChanged?.Invoke();
    }

    private void UpdateOnSurface()
    {
        float radius = _collider.size.x / 2;
        Vector2 origin = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y + radius);
        IEnumerable<RaycastHit2D> hits = Physics2D
            .CircleCastAll(origin, radius, Vector2.down, _onSurfaceCastDistance, _groundLayer);

        AllContacts = hits.Select(x => new PointInfo(x.point, x.normal));
    }

    private void UpdateNotOnSurface()
    {
        List<ContactPoint2D> points = new List<ContactPoint2D>();

        _collider.GetContacts(points);
        AllContacts = points.Select(x => new PointInfo(x.point, x.normal));
    }

    public Vector2 Product(Vector2 direction)
    {
        IEnumerable<PointInfo> points = AllContacts
            .Where(x => IsValidNormal(x.Normal))
            .OrderBy(x => transform.position.x.CompareTo(x.Position.x));

        PointInfo point = direction.x < 0 ?
            points.LastOrDefault() :
            points.FirstOrDefault();

        return direction - Vector2.Dot(direction, point.Normal) * point.Normal;
    }

    public bool IsValidNormal(Vector2 normal)
    {
        return GetAbsoluteDegrees(normal) < _maxNormalDegrees;
    }

    private float GetAbsoluteDegrees(Vector2 normal)
    {
        return Mathf.Atan2(Mathf.Abs(normal.x), Mathf.Abs(normal.y)) * Mathf.Rad2Deg;
    }
}