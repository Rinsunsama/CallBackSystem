
//计时系统
using UnityEngine;
using System.Collections.Generic;
using System;

public class TimerCtr : MonoBehaviour {

    public static TimerCtr Instance;
    public static readonly string obj = "lock";

    private Timer myTimer = new Timer();


    private void Start()
    {
        myTimer.SetLog((string info) =>
        {
            Debug.Log("TimerLog: " + info);
        });
        Instance = this;
    }

    private void Update()
    {
        myTimer.Update();

    }

    #region TimeTask
    //添加定时任务
    public int AddTimeTask(Action callBack,float delay,ETimeUnit type = ETimeUnit.Millisecond, int count = 1)
    {
        return myTimer.AddTimeTask(callBack, delay, type, count);
    }

    //删除定时任务
    public bool DelTimeTask(int tID)
    {
        return myTimer.DelTimeTask(tID);
    }

    //替换定时任务
    public bool ReplaceTimeTask(int tID,Action callBack, float delay, ETimeUnit type = ETimeUnit.Millisecond, int count = 1)
    {
        return myTimer.ReplaceTimeTask(tID, callBack, delay, type, count);
    }

    #endregion

    #region FrameTask
    //添加定帧任务
    public int AddFrameTask(Action callBack, int delay,int count = 1)
    {
        return myTimer.AddFrameTask(callBack, delay, count);
    }

    //删除定帧任务
    public bool DelFrameTask(int tID)
    {
        return myTimer.DelFrameTask(tID);
    }

    //替换定帧任务
    public bool ReplaceFrameTask(int tID, Action callBack, int delay,int count = 1)
    {
        return myTimer.ReplaceFrameTask(tID, callBack, delay, count);
    }

    #endregion

}
