using UnityEngine;

[ExecuteInEditMode]
public class ConnectionLine : MonoBehaviour
{
    [SerializeField] private SkillNode startNode;
    [SerializeField] private SkillNode endNode;
    [SerializeField] private float width = 15f;

    private void Start()
    {
        transform.position = startNode.transform.position;
        transform.rotation = Quaternion.Euler(0,0, GetAngle(startNode.transform.position, endNode.transform.position));
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 2 * Vector2.Distance(startNode.transform.position, endNode.transform.position));
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.y) * (180 / Mathf.PI));
    }
}
