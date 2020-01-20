using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Item의 이름
    public string iName;

    // 슬롯 당 Itme 최대 소지 가능 수
    public int maxNumber;

    // Item의 sprite
    public Sprite sprite { get; set; }

    private void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        drop();
    }
    void drop() {

    }

}

