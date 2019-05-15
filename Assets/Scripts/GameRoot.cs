using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
    public void OnClickBtn()
    {
        Debug.Log("Add TimerTask");
        TimerCtr.Instance.AddTimeTask(FuncA,0.001f,ETimeUnit.Hour,2);
    }

    public void FuncA()
    {
        Debug.Log("do something");
    }
}
