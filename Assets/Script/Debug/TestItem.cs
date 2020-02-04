using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    int i=13;
    public override void use()
    {
        Debug.Log("자식");
        Debug.Log(i);
        num--;
    }
}
