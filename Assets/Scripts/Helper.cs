using UnityEngine;

/// <summary>
/// Class for various helper functions
/// </summary>
public class Helper
{
    /// <summary>
    /// Calculates the end position of a transform
    /// </summary>
    /// <param name="objTransform">The transform to use</param>
    /// <returns>End position coordinates</returns>
    public static Vector3 CalculateEndPosition(Transform objTransform)
    {
        return objTransform.position + objTransform.right * objTransform.lossyScale.x;
    }
}
