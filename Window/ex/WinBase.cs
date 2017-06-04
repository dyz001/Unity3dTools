using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.window
{
    public class WinBase
    {
        protected GameObject m_winObj;

        protected bool m_isInitControl;
        protected Rect m_titleRect = new Rect(0,0,300,20);
        //窗体初始化，实例化等操作
        public void Init()
        {
            m_isInitControl = false;
            InitControls();
            AssignEvents();
            WinManager.GetInstance().AddWindow(this);
        }

        protected virtual Rect GetTitleRect()
        {
            //RectTransform rectTrans = m_winObj.GetComponent<RectTransform>();
            //Rect TitleRect = new Rect(rectTrans.x)
            return new Rect();
        }

        public void SetWinObj(GameObject winObj)
        {
            m_winObj = winObj;
        }
        //初始化控件
        protected virtual void InitControls()
        {

        }

        protected virtual void AssignEvents()
        {

        }
        public bool IsShow()
        {
            return m_winObj.activeSelf;
        }
        protected virtual void OnShow()
        {

        }
        protected virtual void OnHide()
        {

        }

        public virtual void Update()
        {

        }

        protected void OnWinDrag(int deltaX, int deltaY)
        {
            Vector3 pos = m_winObj.transform.localPosition;
            pos.x += deltaX;
            pos.y += deltaY;
        }

        public void Show()
        {
            if (m_winObj == null) return;
            m_winObj.SetActive(true);
        }
        public void Hide()
        {
            if (!m_winObj) return;
            m_winObj.SetActive(false);
        }
    }
}
