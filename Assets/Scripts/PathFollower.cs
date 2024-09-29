using UnityEngine;

// base code from https://www.youtube.com/watch?v=TkVR2Mf_U8Y
public class PathFollower : MonoBehaviour
{
    public float speed = 3f;
    public Transform pathParent;
    Transform targetObject;
    int index = 0;
    Vector3 targetPos;
    bool reachedTarget = false;

    private void OnEnable()
    {
        FenceBuilder.OnRedrawFinished += UpdateTargetPos;
    }

    private void OnDisable()
    {
        FenceBuilder.OnRedrawFinished -= UpdateTargetPos;
    }

    /// <summary>
    /// Updates the target position.
    /// </summary>
    private void UpdateTargetPos()
    {
        // first time - set target and position
        if (targetObject == null)
        {
            if (pathParent.childCount > index)
            {
                targetObject = pathParent.GetChild(index);
                transform.position = AdjustPosition(targetObject.position); // 
                targetPos = AdjustPosition(Helper.CalculateEndPosition(targetObject));
                reachedTarget = true;
            }
            else return;
        }

        if (reachedTarget)
        {
            targetPos = AdjustPosition(Helper.CalculateEndPosition(targetObject));        
        }
        else
        {
            targetPos = AdjustPosition(targetObject.position);
        }
    }

    /// <summary>
    /// Function to adjust the position for our camera
    /// </summary>
    /// <param name="position">Position to adjust</param>
    /// <returns>Adjusted position</returns>
    private Vector3 AdjustPosition(Vector3 position)
    {
        return new Vector3(position.x, position.y + 2f, position.z - 1f);
    }

    void Update()
    {
        // don't do anything if we have no target
        if (targetObject == null) return;

        // move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // if at the target, get next target
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (reachedTarget)
            {
                do
                {
                    index++;
                    index %= pathParent.childCount;
                    targetObject = pathParent.GetChild(index);
                }
                while (!targetObject.gameObject.activeSelf);
                reachedTarget = false;
            }
            else reachedTarget = true;
            UpdateTargetPos();
        }
    }
}