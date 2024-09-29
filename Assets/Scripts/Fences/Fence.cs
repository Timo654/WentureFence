using System;
using UnityEngine;

/// <summary>
/// A single fence object
/// </summary>
[Serializable]
public class Fence
{
    public int fenceID = -1; // only used when updating/removing elements. -1 is unspecified
    [Range(0f, 180f)] public float angle = 180f;
    public float length = 1f;

    public Fence(float angle, float length)
    {
        this.angle = angle;
        this.length = length;
    }
}
