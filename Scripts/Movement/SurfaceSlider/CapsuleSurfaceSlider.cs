using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CapsuleSurfaceSlider : MonoBehaviour, ISurfaceSlider
{
    [SerializeField] private float _maxNormalX = 0.8f;
    [SerializeField] private float _castDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer = 1 << 6;
    private CapsuleCollider2D _collider;

    public Vector2 Normal { get; set; }
    public bool IsOnSurface { get { return Normal != default; } }
    public List<ContactPoint2D> AllContacts { get; private set; } = new List<ContactPoint2D>();

    private void Start()
    {
        TryGetComponent(out _collider);
    }

    private void FixedUpdate()
    {
        UpdateNormal();
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
        UpdateNormal();
        _collider.GetContacts(AllContacts); 
    }

    private void UpdateNormal()
    {
        float radius = _collider.size.x/2;
        Vector2 origin = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y + radius);

        RaycastHit2D hit = Physics2D.CircleCastAll(origin, radius, Vector2.down, _castDistance, _groundLayer)
            .Where(x => IsValidNormal(x.normal))
            .OrderByDescending(x => Mathf.Abs(x.normal.x))
            .FirstOrDefault();
        
        Normal = hit.normal;
    }

    public Vector2 Product(Vector2 direction)
    {
        return direction - Vector2.Dot(direction, Normal) * Normal;
    }

    public bool IsValidNormal(Vector2 normal)
    {
        return normal.y > 0 && Mathf.Abs(normal.x) < _maxNormalX;
    }
}