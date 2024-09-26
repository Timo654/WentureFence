using System;
using UnityEngine;

/// <summary>
/// A single fence object
/// </summary>
[Serializable]
public class Fence
{
    [Range(0f, 180f)] public float angle = 180f;
    public float length = 1f;

    public Fence(float angle, float length)
    {
        this.angle = angle;
        this.length = length;
    }
}
