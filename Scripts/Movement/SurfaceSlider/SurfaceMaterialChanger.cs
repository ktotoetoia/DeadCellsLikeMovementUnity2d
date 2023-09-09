﻿using UnityEngine;

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

        _slider.OnNormalChanged += UpdatePhysicsMaterial;

        if (TryGetComponent(out ICanJump canJump))
        {
            canJump.OnJumpPerformed += () => _collider.sharedMaterial = _inAirMaterial;
        }
    }

    private void UpdatePhysicsMaterial()
    {
        _collider.sharedMaterial = _slider.IsOnSurface ? _onSurfaceMaterial : _inAirMaterial;
    }
}