using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : Slot, IPointerClickHandler
{
    public int id;
    public Color selectedColor;
    public bool selected = false;

    public void setId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return id;
    }

    public void firstSelected()
    {
        border.color = selectedColor;
    }

    public void select()
    {
        border.color = selectedColor;
        FindObjectOfType<Inventory>().reportSelected(id);
    }

    public void unSelect()
    {
        border.color = originColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select();
    }
}
