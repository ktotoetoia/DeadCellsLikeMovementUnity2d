using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SurfaceSlider : MonoBehaviour, ISurfaceSlider
{
    [SerializeField] private float _maxNormalX = 0.8f;
    [SerializeField] private PhysicsMaterial2D _defaultMaterial;
    [SerializeField] private PhysicsMaterial2D _onSlopeMaterial;
    private Collider2D _collider;

    public Vector2 Normal { get; set; }
    public bool IsOnSurface { get { return Normal != default; } }

    private void Start()
    {
        TryGetComponent(out _collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateNormal();
        UpdatePhysicsMaterial();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateNormal();
        UpdatePhysicsMaterial();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UpdateNormal();
        UpdatePhysicsMaterial();
    }

    private void UpdateNormal()
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();

        _collider.GetContacts(contacts);

        Normal = contacts.LastOrDefault(x => Mathf.Abs(x.normal.x) < _maxNormalX && x.normal.y > 0).normal;
    }

    private void UpdatePhysicsMaterial()
    {
        if (Normal == default)
        {
            _collider.sharedMaterial = _defaultMaterial;

            return;
        }

        _collider.sharedMaterial = _onSlopeMaterial;
    }

    public Vector2 Product(Vector2 direction)
    {
        return direction - Vector2.Dot(direction, Normal) * Normal;
    }
}