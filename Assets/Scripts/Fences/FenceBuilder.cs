using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for handling the fence building
/// </summary>
public class FenceBuilder : MonoBehaviour
{
    [SerializeField] private GameObject fencePrefab; // it would be possible to make a system for multiple fence types, but that's out of the scope for now
    public List<Fence> fenceList;
    private static event Action OnSettingsChanged;

    // not sure if this is a good idea, but I'll do this for now.
    private void OnEnable()
    {
        OnSettingsChanged += Redraw;
    }
    private void OnDisable()
    {
        OnSettingsChanged -= Redraw;
    }

    // for debugging purposes in editor
    void Update()
    {
        if (!Application.isEditor) return;

        if (Input.GetKeyUp(KeyCode.H))
        {
            OnSettingsChanged?.Invoke();
        }
    }

    /// <summary>
    /// Adds a new fence to the fence list.
    /// </summary>
    /// <param name="angle">Interior angle of the fence</param>
    /// <param name="length">Length of the fence in metres.</param>
    public void AddFence(float angle, float length)
    {
        // basic error checking
        if (angle < 0f || angle > 180f)
        {
            Debug.LogWarning($"User tried to input invalid angle values, expected value should be between 0 to 180. Got {angle}.");
            return; // ignore invalid inputs
        }
        if (length < 0f)
        {
            Debug.LogWarning("Length cannot be negative!");
            return;
        }
        fenceList.Add(new Fence(angle, length));
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Removes the specified fence from the fence list.
    /// </summary>
    /// <param name="fenceIndex">Index of the fence to remove</param>
    public void RemoveFence(int fenceIndex)
    {
        if (fenceIndex > fenceList.Count || fenceIndex < 0)
        {
            Debug.LogWarning($"Invalid fence index! Got {fenceIndex}.");
            return;
        }
        fenceList.RemoveAt(fenceIndex);
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Redraws the fence.
    /// </summary>
    void Redraw()
    {
        // TODO - could be optimised by not redrawing the entire thing for a single change. would need to check what exactly changed and only change
        // based on that.
        Vector3 prevPos = Vector3.zero;
        float currAngle = 0f;
        for (int i = 0; i < fenceList.Count; i++)
        {
            Fence fence = fenceList[i];
            currAngle += (180f - fence.angle); // user specifies the interior angles, so we convert them
            GameObject go;
            if (i >= transform.childCount)
            {
                go = Instantiate(fencePrefab, prevPos, Quaternion.Euler(0f, currAngle, 0f), transform);
            }
            else
            {
                go = transform.GetChild(i).gameObject;
                go.transform.SetPositionAndRotation(prevPos, Quaternion.Euler(0f, currAngle, 0f));
                go.SetActive(true);
            }
            go.transform.localScale = new(fence.length, go.transform.lossyScale.y, go.transform.lossyScale.z);
            prevPos = Helper.CalculateEndPosition(go.transform);
        }

        if (transform.childCount > fenceList.Count)
        {
            for (int i = fenceList.Count; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
