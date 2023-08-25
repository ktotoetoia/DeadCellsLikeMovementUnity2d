using UnityEngine;

[RequireComponent(typeof(CapsuleSurfaceSlider))]
public class SurfaceMaterialChanger : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _inAirMaterial;
    [SerializeField] private PhysicsMaterial2D _onSurfaceMaterial;
    private Collider2D _collider;
    private ISurfaceSlider _slider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _slider = GetComponent<ISurfaceSlider>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdatePhysicsMaterial();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UpdatePhysicsMaterial();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdatePhysicsMaterial();
    }

    private void UpdatePhysicsMaterial()
    {
        if (!_slider.IsOnSurface)
        {
            _collider.sharedMaterial = _inAirMaterial;

            return;
        }

        _collider.sharedMaterial = _onSurfaceMaterial;
    }
}