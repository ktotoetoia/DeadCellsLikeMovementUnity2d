using System.Collections.Generic;
using UnityEngine;

public interface ISurfaceSlider
{
    event System.Action<Vector2> OnNormalChanged;

    List<ContactPoint2D> AllContacts { get; }
    Vector2 Normal { get; set; }
    bool IsOnSurface { get; }

    Vector2 Product(Vector2 direction);
    bool IsValidNormal(Vector2 normal);
    void ResetNormal();
}