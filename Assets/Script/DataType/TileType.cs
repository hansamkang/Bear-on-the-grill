using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileType
{
    public string name;
    public float height;
    public float temperature;
    public float humidity;
    public TileBase tileBase;
}
