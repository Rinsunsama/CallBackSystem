using System;

//定时时间任务
public class TimeTask
{
    public double deskTime;  //触发时间
    public double delay;     //延时时间
    public Action callBack; 
    public int count;       //循环次数，0表示无限循环
    public int tID;         //每个计时任务唯一ID
    public TimeTask(Action callBack, double deskTime,int count, double delay,int tID)
    {
        this.deskTime = deskTime;
        this.callBack = callBack;
        this.count = count;
        this.delay = delay;
        this.tID = tID;
    }
}

//定时帧任务
public class FrameTask
{
    public int deskFrame;  //触发时间
    public int delay;     //延时时间
    public Action callBack;
    public int count;       //循环次数，0表示无限循环
    public int tID;         //每个计时任务唯一ID
    public FrameTask(Action callBack, int deskFrame, int count, int delay, int tID)
    {
        this.deskFrame = deskFrame;
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