using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    private Transform targetTransform;
    [SerializeField] private bool followRoatation;

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    private void LateUpdate()
    {
        if(targetTransform == null) return;

        transform.position = targetTransform.position;
        if(followRoatation ) transform.rotation = targetTransform.rotation;
    }
}
