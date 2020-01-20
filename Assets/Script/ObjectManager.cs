using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectManager : MonoBehaviour
{
    public Player player;
    public ObjectRatio[] grobalObjectRatio;
    public RegionObjectRatio [] regionObjectRatio;
    public MapManager mapManager;
    public Tilemap objectTileMap;

    // object의 id와 name의 key, value hashmap
    Dictionary<int, string> objectDictionary;
    // TileMap에 표현되어있는 Sector들의 LIst
    List<Vector2> sectorLog = new List<Vector2>();

    Map mapData;
    int mapSize;
    int sectorSize;
    int sectorOffset;

    // Current Player Sector 현재 플레이어의 섹터를 저장
    Vector2 cps;

    // Start is called before the first frame update
    void Start()
    {
        objectDictionary = new Dictionary<int, string>();
        mapManager = FindObjectOfType<MapManager>();
        mapData = mapManager.getMapData();
        sectorSize = mapManager.sectorSize;
        mapSize = mapManager.mapSize;
        sectorOffset = mapManager.sectorOffset;

        // 오브젝트 맵 데이터 생성후 MapData에 전달.
        mapData.setObjectMap(generateObjectMap());

        // ObjectList 생성
        generateObjectList();

        // 플레이어가 위치한 섹터 파악 후 해당 섹터 표현
        cps = player.getSector(sectorSize);
        drawSector(cps);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 플레이어가 위치한 섹터 파악
        cps = player.getSector(sectorSize);

        // 플레이어의 위치가 Sector를 Prefetch 해야하는지 확인
        int checkChangeX, checkChangeY;
        checkChangeX = player.checkChangeSectorX(sectorSize, sectorOffset);
        checkChangeY = player.checkChangeSectorY(sectorSize, sectorOffset);

        // 플레이어의 위치가 Sector를 Prefetch 해야한다면 해당 위치에 Prefetch 함
        if (checkChangeX == -1)
        {
            drawSector(new Vector2(cps.x - 1, cps.y));
        }
        else if (checkChangeX == 1)
        {
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

        if (checkChangeX == -1 && checkChangeY == -1)
        {
            drawSector(new Vector2(cps.x - 1, cps.y - 1));
        }
        else if (checkChangeX == -1 && checkChangeY == 1)
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

    // ObjectMap을 생성후 반환
    int[,] generateObjectMap()
    {
        int[,] map = new int[mapSize, mapSize];
        int num = 0;
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                num++;
                
                bool grobalSet = false;
                int rand, t, region;
                rand = Random.Range(0, 10000);

                t = 0;
                
                // Grobal Obejct들 생성
                for(int i =0; i<grobalObjectRatio.Length; i++)
                {
                    t += grobalObjectRatio[i].percentage;
                    if (rand < t)
                    {                       
                        map[x, y] = grobalObjectRatio[i].objectId;
                        grobalSet = true;
                        break;
                    }
                }

                if (grobalSet) continue;

                region = mapData.getRegionPoint(x, y);

                // region object들 생성
                for(int i =0;i<regionObjectRatio[region].objectRatioType.Length; i++)
                {
                    t += regionObjectRatio[region].objectRatioType[i].percentage;
                    if (rand < t)
                    {
                        map[x, y] = regionObjectRatio[region].objectRatioType[i].objectId;
                        break;
                    }
                }
                
            }
        }
        Debug.Log("루틴횟수:" + num);
        return map;
    }

    // ObjectList 생성
    void generateObjectList()
    {
        for(int i =0; i<grobalObjectRatio.Length; i++)
        {
            objectDictionary.Add(grobalObjectRatio[i].objectId, grobalObjectRatio[i].objectName);
        }

        for(int i = 0; i < regionObjectRatio.Length; i++)
        {
            for(int j =0; j< regionObjectRatio[i].objectRatioType.Length; j++)
            {
                objectDictionary.Add(regionObjectRatio[i].objectRatioType[j].objectId, regionObjectRatio[i].objectRatioType[j].objectName);
            }
        }
    }

    // Sector를 입력받아 해당 Sector의 Obeject를 표현한다.
    public void drawSector(Vector2 sector)
    {
        if (sectorLog.Contains(sector)) return;
        for (int y = 0; y < sectorSize; y++)
        {
            for (int x = 0; x < sectorSize; x++)
            {
                int wantX, wantY, i;
                wantX = x + sectorSize * (int)(sector.x + (mapSize / sectorSize / 2));
                wantY = y + sectorSize * (int)(sector.y + (mapSize / sectorSize / 2));

                i = mapData.getObjectMapPoint(wantX, wantY);

                if (i == 0)
                    continue;

                string tileName = objectDictionary[i];
                string path = "Objects/"+tileName;

                TileBase tile = Resources.Load(path) as TileBase;
                objectTileMap.SetTile(new Vector3Int(x - (sectorSize / 2) + (int)(sector.x * sectorSize), y - (sectorSize / 2) + (int)(sector.y * sectorSize), 0), tile);
            }
        }
        sectorLog.Add(sector);
        eraseSector(sector);
    }

    // 입력받은 Sector의 Object를 Tilemap에서 지움
    public void eraseSector(Vector2 sector)
    {
        List<Vector2> deleteList = new List<Vector2>();

        foreach (Vector2 s in sectorLog)
        {
            if (Mathf.Abs(s.x - sector.x) > 1 || Mathf.Abs(s.y - sector.y) > 1)
            {
                for (int y = 0; y < sectorSize; y++)
                {
                    for (int x = 0; x < sectorSize; x++)
                    {
                        objectTileMap.SetTile(new Vector3Int(x - (sectorSize / 2) + (int)(s.x * sectorSize), y - (sectorSize / 2) + (int)(s.y * sectorSize), 0), null);
                    }
                }
                deleteList.Add(s);
            }
        }
        foreach (Vector2 d in deleteList)
        {
            sectorLog.Remove(d);
        }
    }

}
