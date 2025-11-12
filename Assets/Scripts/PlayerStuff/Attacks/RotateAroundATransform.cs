using UnityEngine;

public class RotateAroundATransform : MonoBehaviour
{
    private Transform targetTransform;
    private float rotationSpeed;

    public void SetTargetTransform(Transform targetTransform, float speed)
    {
        this.targetTransform = targetTransform;
        this.rotationSpeed = speed;
    }

    private void LateUpdate()
    {
        if (targetTransform == null) return;

        transform.Rotate(targetTransform.forward, rotationSpeed * Time.deltaTime);
    }
}
