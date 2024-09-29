using System;
using UnityEngine;

public class ReactHandler : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject targetCamera;
    public static event Action<Fence> OnAddFence;
    public static event Action<int> OnRemoveFence;

    public void AddFence(string jsonMsg)
    {
        var fenceObj = JsonUtility.FromJson<Fence>(jsonMsg);
        OnAddFence?.Invoke(fenceObj);
    }

    public void RemoveFence(string jsonMsg)
    {
        // TODO - remove fence
        var fenceObj = JsonUtility.FromJson<FenceID>(jsonMsg);
        OnRemoveFence?.Invoke(fenceObj.fenceID);
    }

    public void ChangeCameraMode(string jsonMsg)
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        targetCamera.SetActive(!targetCamera.activeSelf);
    }
}

// only exists for JSON purposes
[Serializable]
public class FenceID
{
    public int fenceID;
}