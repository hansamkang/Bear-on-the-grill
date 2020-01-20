using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectC : MonoBehaviour
{
    public int objectId;
    public int durability;
    // Interpace로 만들자
    public bool enableDestroy=true;
    public bool thisTrigger=false;

    Collider2D objectCollider;

    TileBase tileBase;

    private void Start()
    {
        objectC_init();
    }

    void FixedUpdate()
    {
        objectC_run();
    }

    protected void objectC_init()
    {
        objectCollider = gameObject.GetComponent<Collider2D>();
        if (thisTrigger)
        {
            objectCollider.isTrigger = thisTrigger;
        }
    }

    protected void objectC_run()
    {
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
