using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {

    int tID = 0;
	// Use this for initialization
	void Start () {
        
	}

    #region TimeTask TestCode
    public void OnClickAddBtn()
    {

        tID = TimerCtr.Instance.AddTimeTask(() =>
        {
            Debug.Log("do something ");
        }, 1000, ETimeUnit.Millisecond, 0);
        Debug.Log("Add TimerTask===TID" + tID);
    }

    public void OnClickDelBtn()
    {

        bool ret = TimerCtr.Instance.DelTimeTask(tID);
        Debug.Log("Delete TimerTask====ret====>" + ret);
    }

    public void OnClickReplaceBtn()
    {
        bool ret = TimerCtr.Instance.ReplaceTimeTask(tID, () =>
        {
            Debug.Log("new => do something ");
        }, 1000, ETimeUnit.Millisecond, 0);
        Debug.Log("Delete TimerTask====ret====>" + ret);
    }
    #endregion

    #region FrameTask TestCode
    public void OnClickFrameAddBtn()
    {

        tID = TimerCtr.Instance.AddFrameTask(() =>
        {
            Debug.Log("frame => do something ");
        }, 30,0);
        Debug.Log("Add FrameTask===TID" + tID);
    }

    public void OnClickFrameDelBtn()
    {

        bool ret = TimerCtr.Instance.DelFrameTask(tID);
        Debug.Log("Delete FrameTask====ret====>" + ret);
    }

    public void OnClickFrameReplaceBtn()
    {
        bool ret = TimerCtr.Instance.ReplaceFrameTask(tID, () =>
        {
            Debug.Log("new frame=> do something ");
        }, 50,0);
        Debug.Log("Replace FrameTask====ret====>" + ret);
    }
    #endregion
}
