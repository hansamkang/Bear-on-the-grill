using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugInformation : MonoBehaviour
{
    public Text pAbsoutePositionText;
    public Text pRelativePositionText;
    public Text pSectorText;

    public GameObject playerGameObeject;
    public MapManager mapManager;
    Transform playerTransform;
    Player player;

    int ax, ay, rx, ry,mapSize, sectorSize;
    Vector2 cps;

    string positionStr, sectorStr;

    // Start is called before the first frame update
    void Start()
    {
        player = playerGameObeject.GetComponent<Player>();
        playerTransform = playerGameObeject.GetComponent<Transform>();
        mapSize = mapManager.mapSize;
        sectorSize = mapManager.sectorSize;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ax = (int)playerTransform.position.x;
        ay = (int)playerTransform.position.y;
        pAbsoutePositionText.text = "AbsoutePosition(x:"+ax+" y:" +ay+")";
        rx = ax + mapSize / 2;
        ry = ay + mapSize / 2;
        pRelativePositionText.text = "RelativePosition(x:" + rx + " y:" + ry + ")";
        cps = player.getSector(sectorSize);
        pSectorText.text = "Sectgor(" + cps.x + ", " + cps.y + ")";
    }
}
