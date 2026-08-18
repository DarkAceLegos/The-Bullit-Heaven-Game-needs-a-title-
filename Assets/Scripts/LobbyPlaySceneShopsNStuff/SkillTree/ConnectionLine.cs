using UnityEngine;

//[ExecuteInEditMode]
public class ConnectionLine : MonoBehaviour
{
    [SerializeField] public SkillNode startNode;
    [SerializeField] public SkillNode endNode;
    [SerializeField] private float width = 15f;

    private void Start()
    {
        transform.position = (startNode.transform.position + endNode.transform.position) / 2;
        transform.rotation = Quaternion.Euler(0, 0, GetAngle(startNode.transform.position, endNode.transform.position) - 90);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 2 * Vector2.Distance(startNode.transform.position, endNode.transform.position));        
    }

    private void Update()
    {
        /*transform.position = (startNode.transform.position + endNode.transform.position) / 2;
        transform.rotation = Quaternion.Euler(0, 0, GetAngle(startNode.transform.position, endNode.transform.position) - 90);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 2 * Vector2.Distance(startNode.transform.position, endNode.transform.position));//*/
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
