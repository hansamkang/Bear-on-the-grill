using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public  Slot slot;
    public Image image;
    public Transform startParent;
    public void Start()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = transform.parent;
        transform.SetParent(GameObject.Find("Canvas").transform);
        image.raycastTarget = false;
    }

    // IDragHander 인터페이스 구현 메소드
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    // IEndDragHandler 인터페이스 구현 메소드
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject layObject = eventData.pointerCurrentRaycast.gameObject;

        if (layObject !=null && layObject.CompareTag("Slot"))
        {
            Slot targetSlot = layObject.GetComponentInParent<Slot>();
            Item tempItem = slot.getItem();
            int tempNum = slot.getNum();

            Debug.Log(targetSlot.getItem());
            Debug.Log(slot.getItem());
            slot.setItem(targetSlot.getItem());
            targetSlot.setItem(tempItem);

            slot.updateSlot();
            targetSlot.updateSlot();
        }
        else
        {
            Debug.Log("엘스");
        }

        transform.SetParent(startParent);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
