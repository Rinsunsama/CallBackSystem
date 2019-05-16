
//计时系统
using UnityEngine;
using System.Collections.Generic;
using System;

public class TimerCtr : MonoBehaviour {

    public static TimerCtr Instance;
    public static readonly string obj = "lock";

    //定时任务
    private List<TimeTask> timeTaskList = new List<TimeTask>();
    private List<TimeTask> tempTimeTaskList = new List<TimeTask>();         //缓存定时器任务
    private int tID = 0;
    private List<int> tIDList = new List<int>();
    private List<int> recycleIDList = new List<int>();

    //定帧任务
    private int curFrame = 0;
    private List<FrameTask> frameTaskList = new List<FrameTask>();
    private List<FrameTask> tempFrameTaskList = new List<FrameTask>();         //缓存定时器任务
    //private int tID = 0;
    //private List<int> tIDList = new List<int>();
    //private List<int> recycleIDList = new List<int>();


    void Awake() {
        Instance = this;
    }

    private void Update()
    {
        curFrame++;
        CheckFrameTask();
        CheckTimeTask();
    }

    #region TimeTask
    //添加定时任务
    public int AddTimeTask(Action callBack,float delay,ETimeUnit type = ETimeUnit.Millisecond, int count = 1)
    {
        delay *= (int)type;
        int tID = GetTID();
        TimeTask timeTask = new TimeTask(callBack, Time.realtimeSinceStartup*1000 + delay, count,delay,tID);
        tempTimeTaskList.Add(timeTask);
        tIDList.Add(tID);
        return tID;
    }

    //删除定时任务
    public bool DelTimeTask(int tID)
    {
        bool find = false;
        for (int i = 0; i < timeTaskList.Count; i++)
        {
            if (tID == timeTaskList[i].tID)
            {
                timeTaskList.RemoveAt(i);
                find = true;
                break;
            }
        }
        if (!find)
        {
            for (int i = 0; i < tempTimeTaskList.Count; i++)
            {
                if (tID == tempTimeTaskList[i].tID)
                {
                    tempTimeTaskList.RemoveAt(i);
                    find = true;
                    break;
                }
            }
        }
        if (find)
        {
            for (int i = 0; i < tIDList.Count; i++)
            {
                if (tID == tIDList[i])
                {
                    tIDList.RemoveAt(i);
                    recycleIDList.Add(tID);
                    break;
                }
            }
        }
        return find;
    }

    //替换定时任务
    public bool ReplaceTimeTask(int tID,Action callBack, float delay, ETimeUnit type = ETimeUnit.Millisecond, int count = 1)
    {
        delay *= (int)type;
        bool find = false;
        TimeTask timeTask = new TimeTask(callBack, Time.realtimeSinceStartup * 1000 + delay, count, delay, tID);
        //替换
        for (int i = 0; i < timeTaskList.Count; i++)
        {
            if(tID == timeTaskList[i].tID)
            {
                timeTaskList[i] = timeTask;
                find = true;
                break;
            }
        }
        for (int i = 0; i < tempTimeTaskList.Count; i++)
        {
            if (tID == tempTimeTaskList[i].tID)
            {
                tempTimeTaskList[i] = timeTask;
                find = true;
                break;
            }
        }
        return find;
    }

    //检测定时任务
    public void CheckTimeTask()
    {
        //将缓存区的定时器加入列表
        for (int i = 0; i < tempTimeTaskList.Count; i++)
        {
            timeTaskList.Add(tempTimeTaskList[i]);
        }
        tempTimeTaskList.Clear();

        for (int i = 0; i < timeTaskList.Count; i++)
        {
            TimeTask tt = timeTaskList[i];
            if (Time.realtimeSinceStartup * 1000 < tt.deskTime)
            {
                continue;
            }
            Action callBack = timeTaskList[i].callBack;
            try
            {
                if (callBack != null)
                {
                    callBack();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            };

            if (tt.count == 1)
            {
                recycleIDList.Add(timeTaskList[i].tID);
                timeTaskList.RemoveAt(i);
                i--;
            }
            else
            {
                if (tt.count != 0)
                    tt.count--;
                tt.deskTime += tt.delay;
            }
        }
        if (recycleIDList.Count > 0)
        {
            RecycleTIDList();
        }
    }
    #endregion

    #region FrameTask
    //添加定帧任务
    public int AddFrameTask(Action callBack, int delay,int count = 1)
    {
        int tID = GetTID();
        FrameTask frameTask = new FrameTask(callBack, curFrame + delay, count, delay, tID);
        tempFrameTaskList.Add(frameTask);
        tIDList.Add(tID);
        return tID;
    }

    //删除定帧任务
    public bool DelFrameTask(int tID)
    {
        bool find = false;
        for (int i = 0; i < frameTaskList.Count; i++)
        {
            if (tID == frameTaskList[i].tID)
            {
                frameTaskList.RemoveAt(i);
                find = true;
                break;
            }
        }
        if (!find)
        {
            for (int i = 0; i < tempFrameTaskList.Count; i++)
            {
                if (tID == tempFrameTaskList[i].tID)
                {
                    tempFrameTaskList.RemoveAt(i);
                    find = true;
                    break;
                }
            }
        }
        if (find)
        {
            for (int i = 0; i < tIDList.Count; i++)
            {
                if (tID == tIDList[i])
                {
                    tIDList.RemoveAt(i);
                    recycleIDList.Add(tID);
                    break;
                }
            }
        }
        return find;
    }

    //替换定帧任务
    public bool ReplaceFrameTask(int tID, Action callBack, int delay,int count = 1)
    {
        bool find = false;
        FrameTask timeTask = new FrameTask(callBack, curFrame + delay, count, delay, tID);
        //替换
        for (int i = 0; i < frameTaskList.Count; i++)
        {
            if (tID == frameTaskList[i].tID)
            {
                frameTaskList[i] = timeTask;
                find = true;
                break;
            }
        }
        for (int i = 0; i < tempFrameTaskList.Count; i++)
        {
            if (tID == tempFrameTaskList[i].tID)
            {
                tempFrameTaskList[i] = timeTask;
                find = true;
                break;
            }
        }
        return find;
    }

    //检测定帧任务
    public void CheckFrameTask()
    {
        //将缓存区的定时器加入列表
        for (int i = 0; i < tempFrameTaskList.Count; i++)
        {
            frameTaskList.Add(tempFrameTaskList[i]);
        }
        tempFrameTaskList.Clear();

        for (int i = 0; i < frameTaskList.Count; i++)
        {
            FrameTask ft = frameTaskList[i];
            if (curFrame < ft.deskFrame)
            {
                continue;
            }
            Action callBack = frameTaskList[i].callBack;
            try
            {
                if (callBack != null)
                {
                    callBack();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            };

            if (ft.count == 1)
            {
                recycleIDList.Add(frameTaskList[i].tID);
                frameTaskList.RemoveAt(i);
                i--;
            }
            else
            {
                if (ft.count != 0)
                    ft.count--;
                ft.deskFrame += ft.delay;
            }
        }
        if (recycleIDList.Count > 0)
        {
            RecycleTIDList();
        }
    }
    #endregion

    #region Tool
    //获取唯一ID（自增）
    public int GetTID()
    {
        tID++;
        if (tID == int.MaxValue)
        {
            tID = 0;
        }
        while (true)
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

    //回收tID
    public void RecycleTIDList()
    {
        for (int i = 0; i < recycleIDList.Count; i++)
        {
            for (int j = 0; i < tIDList.Count; i++)
            {
                if (recycleIDList[i] == tIDList[j])
                {
                    tIDList.RemoveAt(j);
                }
            }
        }

        recycleIDList.Clear();
    } 
    #endregion
}
