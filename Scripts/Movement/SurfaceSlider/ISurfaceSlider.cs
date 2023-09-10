using System.Collections.Generic;
using UnityEngine;

public interface ISurfaceSlider
{
    List<PointInfo> AllContacts { get; }
    bool IsOnSurface { get; }

    Vector2 Product(Vector2 direction);
    bool IsValidNormal(Vector2 normal);
}