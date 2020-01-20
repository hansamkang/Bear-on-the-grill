using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Player player;  
    public Tilemap map;

    public int mapSize;
    public int sectorSize;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public TileType[] regions;

    Map mapData;
    
    // 섹터의 변경 제한값
    public int sectorOffset = 6;

    // TileMap에 표현되어있는 Sector들의 LIst
    List<Vector2> sectorLog = new List<Vector2>();

    // Current Player Sector 현재 플레이어의 섹터를 저장
    Vector2 cps;

    private void OnEnable()
    {
        // 맵 데이터 생성
        mapData = generateMap();
    }
    // Start is called before the first frame update
    void Start()
    {
        // 지역 생성을 위해 설정한 regions를 정렬
        sortRegion();
        //swapRegion(ref regions[0], ref regions[1]);
        debugSort();
        // 플레이어가 위치한 섹터 파악 후 해당 섹터 표현
        cps = player.getSector(sectorSize);
        drawSector(cps);
    }

    /// <summary>
    /// 플레이어어의 위치에 따라서 생성된 맵을 Draw하고
    /// 플레이어의 위치에서 멀리 벗어난 Sector은 Erase함.
    /// </summary>
    private void FixedUpdate()
    {
        // 플레이어가 위치한 Sector 파악
        cps = player.getSector(sectorSize);

        // 플레이어의 위치가 Sector를 Prefetch 해야하는지 확인
        int checkChangeX, checkChangeY;
        checkChangeX = player.checkChangeSectorX(sectorSize, sectorOffset);
        checkChangeY = player.checkChangeSectorY(sectorSize, sectorOffset);

        // 플레이어의 위치가 Sector를 Prefetch 해야한다면 해당 위치에 Prefetch 함
        if(checkChangeX == -1)
        {
            drawSector(new Vector2(cps.x - 1, cps.y));
        }
        else if(checkChangeX == 1){
            drawSector(new Vector2(cps.x + 1, cps.y));
        }

        if (checkChangeY == -1)
        {
            drawSector(new Vector2(cps.x, cps.y - 1));
        }
        else if (checkChangeY == 1)
        {
            drawSector(new Vector2(cps.x, cps.y + 1));
        }
        
        if(checkChangeX == -1 && checkChangeY == -1)
        {
            drawSector(new Vector2(cps.x - 1, cps.y - 1));
        }
        else if(checkChangeX == -1 && checkChangeY == 1)
        {
            drawSector(new Vector2(cps.x - 1, cps.y + 1));
        }
        else if (checkChangeX == 1 && checkChangeY == -1)
        {
            drawSector(new Vector2(cps.x + 1, cps.y - 1));
        }
        else if (checkChangeX == 1 && checkChangeY == 1)
        {
            drawSector(new Vector2(cps.x + 1, cps.y + 1));
        }
    }

    // Map 생성 함수
    Map generateMap()
    {
        // 맵 환경요소값 생성
        float[,] height = Noise.generate(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] temperature = Noise.generate(mapSize, mapSize, seed + 1, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] humidity = Noise.generate(mapSize, mapSize, seed + 2, noiseScale, octaves, persistance, lacunarity, offset);
        int[,] regionMap = new int[mapSize, mapSize];

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                float currentheight = height[x, y];
                float currentTemperature = temperature[x, y];
                float currentHumidity = humidity[x, y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentTemperature <= regions[i].height && currentTemperature <= regions[i].temperature && currentHumidity <= regions[i].humidity)
                    {
                        regionMap[x, y] = i;
                        break;
                    }
                }
            }
        }

        return new Map(mapSize, mapSize, seed, height, temperature, humidity, regionMap);
    }

    // 입력받은 맵을 타일맵에 표현한다.
    void drawSector(Vector2 sector)
    {
        if (sectorLog.Contains(sector)) return;
        for (int y = 0; y < sectorSize; y++)
        {
            for (int x = 0; x < sectorSize; x++)
            {
                int wantX, wantY, i;
                wantX = x + sectorSize * (int)(sector.x + (mapSize / sectorSize / 2));
                wantY = y + sectorSize * (int)(sector.y + (mapSize / sectorSize / 2));

                i = mapData.getRegionPoint(wantX, wantY);
                map.SetTile(new Vector3Int(x - (sectorSize / 2) + (int)(sector.x * sectorSize), y - (sectorSize / 2) + (int)(sector.y * sectorSize), 0), regions[i].tileBase);
            }
        }
        sectorLog.Add(sector);
        eraseSector(sector);
    }

    // 입력받은 Sector의 맵을 TileMap에서 지움.
    void eraseSector(Vector2 sector)
    {
        List<Vector2> deleteList = new List<Vector2>();

        foreach(Vector2 s in sectorLog)
        {
            if (Mathf.Abs(s.x-sector.x) > 1 || Mathf.Abs(s.y-sector.y) > 1){
                for (int y = 0; y < sectorSize; y++)
                {
                    for (int x = 0; x < sectorSize; x++)
                    {
                        map.SetTile(new Vector3Int(x - (sectorSize / 2) + (int)(s.x * sectorSize), y - (sectorSize / 2) + (int)(s.y * sectorSize), 0), null);
                    }
                }
                deleteList.Add(s);
            }
        }
        foreach(Vector2 d in deleteList)
        {
            sectorLog.Remove(d);
        }
    }

    // region을 정렬, 기수정렬 사용
    void sortRegion()
    {
        int n = regions.Length;

        // humidity에 대해 정렬
        for (int i = 1; i < n; i++)
        {
            for (int j = i; j > 0 && regions[j].humidity < regions[j - 1].humidity; j--)
                swapRegion(ref regions[j], ref regions[j - 1]);
        }

        // temperature 에 대해 정렬
        for (int i = 1; i < n; i++)
        {
            for (int j = i; j > 0 && regions[j].temperature < regions[j - 1].temperature; j--)
                swapRegion(ref regions[j], ref regions[j - 1]);
        }

        // height에 대해 정렬
        for (int i = 1; i < n; i++)
        {
            for (int j = i; j > 0 && regions[j].height < regions[j - 1].height; j--)
                swapRegion(ref regions[j], ref regions[j - 1]);
        }
        
        
        
        
    }

    // 정렬 Debug용 함수
    void debugSort()
    {
        for (int i = 0; i < regions.Length; i++) {
            Debug.Log("[" + i + "] : height=" + regions[i].height + " temperature =" + regions[i].temperature + " humidity=" + regions[i].humidity);
        }

    }
    // region 정렬을 위한 swap 함수
    void swapRegion(ref TileType a, ref TileType b)
    {
        TileType temp = b;
        Debug.Log(temp.name);
        b = a;
        Debug.Log(b.name);
        a = temp;
        Debug.Log(a.name);
    }
    public ref Map getMapData()
    {
        return ref mapData;
    }

    // 디버그용 삭제 가능
    public void showDebug()
    {
        
    }
    // 디버그용 삭제 가능
    public void delteTile()
    {
        map.SetTile(new Vector3Int(0, 0, 0), null);
    }

}
