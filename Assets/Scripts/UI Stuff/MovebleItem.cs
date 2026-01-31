using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovebleItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDarg;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDarg = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDarg);
        image.raycastTarget = true;
    }
}
