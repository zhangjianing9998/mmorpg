using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class UIFormBase : MonoBehaviour
    {
        /// <summary>
        /// UI窗体ID
        /// </summary>
        public int UIFormId
        {
            get;
            private set;
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        public byte GroupId
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前的画布
        /// </summary>
        public Canvas CurrCanvas
        {
            get;
            private set;
        }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public float CloseTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 禁用层级管理
        /// </summary>
        public bool DisableUILayer
        {
            get;
            private set;
        }


        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object UserDate
        {
            get;
            private set;
        }

        private void Awake()
        {
            CurrCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {

            OnInit(UserDate);
            Open(UserDate, true);

        }

        internal void Init(int uiFormId, byte groupId, bool disableUILayer, bool isLock, object userData)
        {
            UIFormId = uiFormId;
            GroupId = groupId;
            DisableUILayer = disableUILayer;
            IsLock = isLock;
            UserDate = userData;

        }

        internal void Open(object userData, bool isFormInit = false)
        {
            if (!isFormInit)
            {
                UserDate = userData;
            }

            if (!DisableUILayer)
            {
                //进行层级管理 增加层级
                GameEntry.UI.SetSortingOrder(this, true);
            }

            OnOpen(UserDate);
        }

        public void Close()
        {
            GameEntry.UI.CloseUIForm(this);
        }

        public void ToClose()
        {
            if (!DisableUILayer)
            {
                //进行层级管理 减少层级
                GameEntry.UI.SetSortingOrder(this, false);
            }

            OnClose();

            //先销毁 以后改成对象池
            Destroy(gameObject);
        }

        private void OnDestory()
        {
            OnBeforeDestory();
        }

        protected virtual void OnInit(object userData) { }
        protected virtual void OnOpen(object userData) { }
        protected virtual void OnClose() { }
        protected virtual void OnBeforeDestory() { }
    }
}