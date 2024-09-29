using System;
using UnityEngine;

public class ReactHandler : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject followerCamera;
    public static event Action<Fence> OnAddFence;
    public static event Action<Fence> OnUpdateFence;
    public static event Action<Fence> OnRemoveFence;

    public void AddFence(string jsonMsg)
    {
        var fenceObj = JsonUtility.FromJson<Fence>(jsonMsg);
        OnAddFence?.Invoke(fenceObj);
    }

    public void UpdateFence(string jsonMsg)
    {
        var fenceObj = JsonUtility.FromJson<Fence>(jsonMsg);
        OnUpdateFence?.Invoke(fenceObj);
    }

    public void RemoveFence(string jsonMsg)
    {
        var fenceObj = JsonUtility.FromJson<Fence>(jsonMsg);
        OnRemoveFence?.Invoke(fenceObj);
    }

    public void ChangeCameraMode(string jsonMsg)
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        followerCamera.SetActive(!followerCamera.activeSelf);
    }
}