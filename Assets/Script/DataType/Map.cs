using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
    int mapWidth;
    int mapHeight;
    int seed;

    float[,] height;
    float[,] temperature;
    float[,] humidity;

    int[,] region;
    int[,] objectMap;

    public Map(int mapWidth, int mapHeight, int seed, float[,] height, float[,] temperature, float[,] humidity, int[,] region)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.seed = seed;
        this.height = height;
        this.temperature = temperature;
        this.humidity = humidity;
        this.region = region;
    }

    public float getHeightPoint(int x, int y)
    {
        return height[x, y];
    }

    public float getTemperaturePoint(int x, int y)
    {
        return temperature[x, y];
    }

    public float getHumidityPoint(int x, int y)
    {
        return humidity[x, y];
    }

    public int getRegionPoint(int x, int y)
    {
        return region[x, y];
    }
    
    public int getObjectMapPoint(int x, int y)
    {
        return objectMap[x, y];
    }

    public void setObjectMapPoint(int x, int y, int value)
    {
        objectMap[x, y] = value;
    }

    public void setObjectMap(int [,] objectMap)
    {
        this.objectMap = objectMap;
    }
}
