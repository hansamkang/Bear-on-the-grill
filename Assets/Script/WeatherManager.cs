using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public int windDirection;
    TimeManager timeManager;
    // Start is called before the first frame update

    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        timeManager.ChangeHour += WM_ChangeHour;
        setWindDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 바람 방향 생성
    public void setWindDirection()
    {
        System.Random rand = new System.Random(int.Parse(System.DateTime.Now.ToString("MM")));
        windDirection = rand.Next((int)Direction.North, (int)Direction.NorthWest);
    }

    // timeManager의 시간변경시 날씨 변경
    public void WM_ChangeHour(object sender, ChangeTimeEventArgs e)
    {
        setWindDirection();
    }
}
