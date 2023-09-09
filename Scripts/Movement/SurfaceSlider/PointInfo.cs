using UnityEngine;

public struct PointInfo
{
    public Vector2 Position;
    public Vector2 Normal;

    public PointInfo(Vector2 position, Vector2 normal)
    {
        Position = position;
        Normal = normal;
    }
}