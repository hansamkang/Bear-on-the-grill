using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public int slotNumber;

    int selectedSlot;
    InventorySlot [] slotList;
    Transform dropTransform;

    private void Start()
    {
        dropTransform = GameObject.Find("DropPosition").GetComponent<Transform>();
        slotList = new InventorySlot[slotNumber];
        for (int i = 0; i < slotNumber; i++)
        {
            GameObject child = Instantiate(slotPrefab, transform.position, Quaternion.identity);
            child.transform.SetParent(gameObject.transform);
            slotList[i] = child.GetComponent<InventorySlot>();
            slotList[i].setId(i);
        }
        selectedSlot = 0;
        slotList[0].select();
    }

    public bool addItem(Item item)
    {
        // 슬롯들 안에 해당 아이템을 가지고 있고 더 넣을 공간이 있는지 확인
        for (int i = 0; i < slotNumber; i++)
        {
            if (!slotList[i].contain()) continue;
            if (slotList[i].compare(item) && slotList[i].getNum() < item.maxNumber)
            {
                slotList[i].add();
                slotList[i].updateSlot();
                return true;
            }
        }

        // 빈 슬롯에 새로 넣겠다.
        for (int i = 0; i < slotNumber; i++)
        {
            if (!slotList[i].contain())
            {
                slotList[i].setItem(item);
                slotList[i].updateSlot();
                return true;
            }
        }

        Debug.Log("인벤토리의 자리부족으로 아이템을 루팅 불가능");
        return false;
    }

    public bool contain(Item item)
    {
        for (int i = 0; i < slotNumber; i++)
        {
            if (!slotList[i].contain()) continue;
            if (slotList[i].compare(item) && slotList[i].getNum() < item.maxNumber)
            {
                return true;
            }
        }
        return false;
    }

    public void reportSelected(int id)
    {
        if (selectedSlot == id) return;
        slotList[selectedSlot].unSelect();
        selectedSlot = id;
        slotList[id].select();
    }

    public void selectSlot(int id)
    {
        slotList[id].select();
    }

    public void useInvenItem()
    {
        slotList[selectedSlot].item.use();
    }

    // 단품으로 버림
    public void dropInvenItem()
    {
        if (!slotList[selectedSlot].contain()) return;
        string name = slotList[selectedSlot].item.iName;
        GameObject dropedItem = Instantiate(Resources.Load("Item/" + name, typeof(GameObject)), dropTransform.position, Quaternion.identity) as GameObject;
        dropedItem.GetComponent<Item>().dropForInven();
        slotList[selectedSlot].item.num--;
        slotList[selectedSlot].updateSlot();
    }
}
