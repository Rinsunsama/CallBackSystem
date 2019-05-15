using System;

public class TimeTask
{
    public float deskTime;  //触发时间
    public float delay;     //延时时间
    public Action callBack; 
    public int count;       //循环次数，0表示无限循环
    public int tID;         //每个计时任务唯一ID
    public TimeTask(Action callBack,float deskTime,int count,float delay,int tID)
    {
        this.deskTime = deskTime;
        this.callBack = callBack;
        this.count = count;
        this.delay = delay;
        this.tID = tID;
    }
}

//时间单位
public enum ETimeUnit
{
    Millisecond = 1,
    Second      = 1000,
    Minute      = 60000,
    Hour        = 3600000,
    Day         = 86400000,
}