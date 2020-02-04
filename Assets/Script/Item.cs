using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Item의 이름
    public string iName;

    // 현재 아이템 중첩 숫자
    public int num;

    // 슬롯 당 Itme 최대 소지 가능 수
    public int maxNumber;

    // Item의 sprite
    public Sprite sprite { get; set; }

    protected void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        num = 1;
    }

    public void dropForInven()
    {

    }

    public virtual void use()
    {
        Debug.Log("엄마");
    }
}

public interface UseAble
{
    void use();
}
