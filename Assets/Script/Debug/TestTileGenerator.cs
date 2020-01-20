using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTileGenerator : MonoBehaviour
{
    public Tilemap map;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;
    public TileType[] regions;

    public void GenerateMap()
    {
        map.ClearAllTiles();

        float[,] noiseMap = Noise.generate(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] temperature = Noise.generate(mapWidth, mapHeight, seed + 1, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] humidity = Noise.generate(mapWidth, mapHeight, seed + 2, noiseScale, octaves, persistance, lacunarity, offset);

        //section[0] = new Section(-1, 0, noiseMap, temperature, humidity);

        //float[,] noiseMap = CustomNoise.generate(mapWidth, mapHeight);
        //float[,] temperature = CustomNoise.generate(mapWidth, mapHeight);
        //float[,] Humidity = CustomNoise.generate(mapWidth, mapHeight);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentheight = noiseMap[x, y];
                float currentTemperature = temperature[x, y];
                float currentHumidity = humidity[x, y];           

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentTemperature <= regions[i].height && currentTemperature <= regions[i].temperature && currentHumidity <= regions[i].humidity)
                    {
                        map.SetTile(new Vector3Int(x, y, 0), regions[i].tileBase);
                        break;
                    }
                }
            }
        }
    }
    public void CleanMap()
    {
        autoUpdate = false;
        map.ClearAllTiles();
    }
    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
