using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Inventory inventory;
    public Item testItem;

    public void addItem()
    {
        inventory.addItem(testItem);
    }
    
}
