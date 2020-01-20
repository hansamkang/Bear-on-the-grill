using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int num=0;

    Image image;
    Text text;
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
        num++;
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
        return this.num;
    }

    public void setNum(int num)
    {
        this.num = num;
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
        if (num == 0)
        {
            text.text = "";
        }
        else {
            text.text = num.ToString();
        }
    }

    public void updateSlot()
    {
        updateSprite();
        updateNumberText();
    }
}
