using UnityEngine;

public interface ISurfaceSlider
{
    bool IsOnSurface { get; }
    Vector2 Product(Vector2 direction);
}