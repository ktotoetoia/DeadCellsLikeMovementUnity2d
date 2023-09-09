using System.Collections.Generic;
using UnityEngine;

public interface ISurfaceSlider
{
    event System.Action OnNormalChanged;

    IEnumerable<PointInfo> AllContacts { get; }
    bool IsOnSurface { get; }

    Vector2 Product(Vector2 direction);
    bool IsValidNormal(Vector2 normal);
}