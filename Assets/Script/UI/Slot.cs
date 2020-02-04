using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Item item;

    protected Color originColor;
    protected Image border;
    protected Image image;
    public Inventory inventory;
    protected Text text;

    public void Awake()
    {
        // Inventory 스크립트의 Start루틴에서 border를 건들여서 Awake에 옮김.
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
        border = gameObject.transform.GetChild(0).GetComponent<Image>();
        originColor = border.color;
    }

    public void Start()
    {
        image = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        text = gameObject.transform.GetChild(1).GetComponent<Text>();
        
    }

    public bool contain()
    {
        if (item == null) return false;
        else return true;
    }

    public bool compare(Item item)
    {
        if (this.item.iName == item.iName) return true;
        else return false;
    }

    public void add()
    {
        item.num++;
    }

    public void decrease()
    {
        item.num--;
    }

    public Item getItem()
    {
        return this.item;
    }

    public void setItem(Item item)
    {
        this.item = item;  
    }

    public int getNum()
    {
        return this.item.num;
    }

    public void setNum(int num)
    {
        this.item.num = num;
    }

    public void clearSlot()
    {
        item = null;
    }
    
    public void updateSprite()
    {
        if(contain())
        {
            image.enabled = true;
            image.sprite = item.sprite;
        }
        else
        {
            image.sprite = null;
            image.enabled = false;
        }
    }
    
    public void updateNumberText()
    {
        if (item == null)
        {
            text.text = "";
        }
        else {
            text.text = item.num.ToString();
        }
    }

    public void updateSlot()
    {
        if (item != null)
        {
            if (item.num <= 0)
            {
                item = null;
            }
        }
        updateSprite();
        updateNumberText();
    }
}
