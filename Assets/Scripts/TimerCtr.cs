
//计时系统
using UnityEngine;
using System.Collections.Generic;
using System;

public class TimerCtr : MonoBehaviour {

    public static TimerCtr Instance;

    private List<TimeTask> timeTaskList = new List<TimeTask>();
    private List<TimeTask> tempTaskList = new List<TimeTask>();         //缓存定时器任务

    private int tID = 0;
    private List<int> tIDList = new List<int>();

    public static readonly string obj = "lock";
    void Awake() {
        Instance = this;
    }
    
    //添加定时任务
    public void AddTimeTask(Action callBack,float delay,ETimeUnit type = ETimeUnit.Millisecond, int count = 1)
    {
        delay *= (int)type;
        int tID = GetTID();
        TimeTask timeTask = new TimeTask(callBack, Time.realtimeSinceStartup*1000 + delay, count,delay,tID);
        tempTaskList.Add(timeTask);
    }

    private void Update()
    {
        //将缓存区的定时器加入列表
        for (int i = 0; i < tempTaskList.Count; i++)
        {
            timeTaskList.Add(tempTaskList[i]);
        }
        tempTaskList.Clear();

        for (int i = 0; i < timeTaskList.Count; i++)
        {
            TimeTask tt = timeTaskList[i];
            if (Time.realtimeSinceStartup*1000 < tt.deskTime)
            {
                continue;
            }
            Action callBack = timeTaskList[i].callBack;
            if (callBack != null)
            {
                callBack();
            }
            if (tt.count == 1)
            {
                timeTaskList.RemoveAt(i);
                i--;
            }
            else
            {
                if (tt.count != 0)
                    tt.count--;
                tt.deskTime = Time.realtimeSinceStartup*1000 + tt.delay;
            }
        }
    }

    //获取唯一ID（自增）
    public int GetTID()
    {
        tID++;
        if (tID == int.MaxValue)
        {
            tID = 0;
        }
        while(true)
        {
            bool find = false;
            for (int i = 0; i < tIDList.Count; i++)
            {
                if (tIDList[i] == tID)
                {
                    find = true;
                    break;
                }
            }
            if (find)
            {
                tID++;
            }
            else
            {
                break;
            }
        }
        return tID;
    }
}
