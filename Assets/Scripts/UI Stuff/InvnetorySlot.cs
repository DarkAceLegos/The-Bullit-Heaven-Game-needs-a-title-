using UnityEngine;
using UnityEngine.EventSystems;

public class InvnetorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            MovebleItem movebleItem = dropped.GetComponent<MovebleItem>();
            movebleItem.parentAfterDarg = transform;
        }
    }
}
