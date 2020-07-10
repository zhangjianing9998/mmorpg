using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class UIFormBase : MonoBehaviour
    {
        /// <summary>
        /// UI����ID
        /// </summary>
        public int UIFormId
        {
            get;
            private set;
        }

        /// <summary>
        /// ������
        /// </summary>
        public byte GroupId
        {
            get;
            private set;
        }

        /// <summary>
        /// ��ǰ�Ļ���
        /// </summary>
        public Canvas CurrCanvas
        {
            get;
            private set;
        }

        /// <summary>
        /// �ر�ʱ��
        /// </summary>
        public float CloseTime
        {
            get;
            private set;
        }

        /// <summary>
        /// ���ò㼶����
        /// </summary>
        public bool DisableUILayer
        {
            get;
            private set;
        }


        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsLock
        {
            get;
            private set;
        }

        /// <summary>
        /// �û�����
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
                //���в㼶���� ���Ӳ㼶
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
                //���в㼶���� ���ٲ㼶
                GameEntry.UI.SetSortingOrder(this, false);
            }

            OnClose();

            //������ �Ժ�ĳɶ����
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