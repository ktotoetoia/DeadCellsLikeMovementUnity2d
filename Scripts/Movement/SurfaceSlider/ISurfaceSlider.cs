using System.Collections.Generic;
using UnityEngine;

public interface ISurfaceSlider
{
    bool IsOnSurface { get; }
    List<ContactPoint2D> AllContacts { get; }

    Vector2 Product(Vector2 direction);
    bool IsValidNormal(Vector2 normal);
}