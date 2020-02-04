using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public event EventHandler<ChangeTimeEventArgs> ChangeHour;
    public int hour;
    public int hourPerRealMinute;

    public float changeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChangeHour += TM_ChangeHour;
        hour = 8;
    }

    // Update is called once per frame
    void Update()
    {
        changeTime += Time.deltaTime;

        if(changeTime > hourPerRealMinute)
        {
            hour++;
            changeTime = 0;
            ChangeHour(this, new ChangeTimeEventArgs(hour));
        }
    }

    // Event가 등록 안되는걸 방지하기위해 명시적으로 선언한 함수.
    private void TM_ChangeHour(object sender, ChangeTimeEventArgs hour)
    {
        return;
    }
}

public class ChangeTimeEventArgs : EventArgs
{
    public int hour;
    public ChangeTimeEventArgs(int hour)
    {
        this.hour = hour;
    }
}
