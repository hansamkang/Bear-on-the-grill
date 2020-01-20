using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public int slotNumber;
    Slot [] slotList;

    public Item TestItem;

    private void Start()
    {
        slotList = new Slot[slotNumber];
        for (int i = 0; i < slotNumber; i++)
        {
            GameObject child = Instantiate(slotPrefab, transform.position, Quaternion.identity);
            child.transform.SetParent(gameObject.transform);
            slotList[i] = child.GetComponent<Slot>();
        }
    }

    public void addItem(Item item)
    {
        // 슬롯들 안에 해당 아이템을 가지고 있고 더 넣을 공간이 있는지 확인
        for (int i = 0; i < slotNumber; i++)
        {
            if (!slotList[i].contain()) continue;
            if (slotList[i].compare(item) && slotList[i].num < item.maxNumber)
            {
                slotList[i].add();
                slotList[i].updateSlot();
                return;
            }
        }

        // 빈 슬롯에 새로 넣겠다.
        for (int i = 0; i < slotNumber; i++)
        {
            if (!slotList[i].contain())
            {
                slotList[i].setItem(item);
                slotList[i].add();
                slotList[i].updateSlot();
                return;
            }
        }

        Debug.Log("인벤토리의 자리부족으로 아이템을 루팅 불가능");
    }
    // Debug용 삭제해도 무방
    public void addTest()
    {
        addItem(TestItem);
    }
}
