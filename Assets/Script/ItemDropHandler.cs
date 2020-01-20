using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    Slot slot;

    public void Start()
    {
        slot=gameObject.GetComponent<Slot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invenPanel = transform as RectTransform;

        //아이템 버릴때
        if (!RectTransformUtility.RectangleContainsScreenPoint(invenPanel, Input.mousePosition))
        {
            Debug.Log("In Drop Item");
        }

    }

}
