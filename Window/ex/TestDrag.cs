using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrag : MonoBehaviour {
    protected Rect titleRect = new Rect(0,0,300,50);
    protected bool onDrag = false;
    protected YZLogger myLog = new YZLogger();
    protected Vector3 mousePos;
	// Use this for initialization
	void Start () {
        myLog.logEnabled = false;
        RectTransform rectTrans = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rectTrans.GetWorldCorners(corners);
        myLog.Log("write world corner");
        foreach(var item in corners)
        {
            myLog.Log(item);
        }
        rectTrans.GetLocalCorners(corners);
        myLog.Log("write local corner");
        foreach (var item in corners)
        {
            myLog.Log(item);
        }

        myLog.Log("my rect:" + rectTrans.rect);
	}
	
    Rect GetTitleRect()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        rectTrans.GetWorldCorners(corners);
        Rect ret = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[1].y - corners[0].y);
        return ret;
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if(GetTitleRect().Contains(Input.mousePosition))
            {
                onDrag = true;
                mousePos = Input.mousePosition;
            }
        }
        if(Input.GetMouseButtonUp(0) && onDrag)
        {
            onDrag = false;
        }
        if(onDrag)
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            Vector3 deltaPos = Input.mousePosition - mousePos;
            mousePos = Input.mousePosition;
            pos.x += deltaPos.x;
            pos.y += deltaPos.y;
            GetComponent<RectTransform>().anchoredPosition = pos;
        }
	}
}
