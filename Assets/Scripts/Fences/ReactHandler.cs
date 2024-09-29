using System;
using UnityEngine;

public class ReactHandler : MonoBehaviour
{
    public static event Action<Fence> OnAddFence;
    public static event Action<int> OnRemoveFence;
    public static event Action OnSwapCameraMode;
    public void AddFence(string jsonMsg)
    {
        var fenceObj = JsonUtility.FromJson<Fence>(jsonMsg);
        OnAddFence?.Invoke(fenceObj);
    }

    public void RemoveFence(string jsonMsg)
    {
        // TODO - remove fence
        Debug.Log("remove fence unimplemented");
    }

    public void ChangeCameraMode(string jsonMsg)
    {
        // TODO - change camera mode
        Debug.Log("camera mode unimplemented");
    }
}
