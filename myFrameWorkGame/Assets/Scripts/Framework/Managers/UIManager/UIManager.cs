using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class UIManager : ManagerBase
    {
        /// <summary>
        /// 存储已经打开的ui链表
        /// </summary>
        private LinkedList<UIFormBase> m_OpenUIList;


        public UIManager()
        {
            m_OpenUIList = new LinkedList<UIFormBase>();
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <param name="userData"></param>
        internal void OpenUIForm(int uiFormId, object userData = null)
        {

            if (IsExists(uiFormId))
            {
                return;
            }


            //读表
            Sys_UIFormEntity entity = GameEntry.DataTable.DataTableManager.Sys_UIFormDBModel.Get(uiFormId);

            if (entity == null)
            {
                Debug.LogError(uiFormId + "    当前表不存在");
                return;
            }

#if DISABLE_ASSETBUNDLE && UNITY_EDITOR


            UIFormBase formBase = GameEntry.UI.Dequeue(uiFormId);
            if (formBase == null)
            {

                string assetString = string.Empty;
                switch (GameEntry.Localization.CurrLanguage)
                {
                    case GameLanguage.Chinese:
                        assetString = entity.AssetPath_Chinese;
                        ; break;
                    case GameLanguage.English:
                        assetString = entity.AssetPath_English;
                        ; break;
                    default:
                        break;
                }


                string path = string.Format("Assets/Download/UI/UIPrefab/{0}.prefab", assetString);

                //加载镜像
                Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);

                GameObject uiObj = Object.Instantiate(obj) as GameObject;
                uiObj.SetParent(GameEntry.UI.GetUIGroup(entity.UIGroupId).Group);
                uiObj.transform.localPosition = Vector3.zero;
                uiObj.transform.localScale = Vector3.one;

                formBase = uiObj.GetComponent<UIFormBase>();

                formBase.Init(uiFormId, entity.UIGroupId, entity.DisableUILayer == 1, entity.IsLock == 1, userData);
            }
            else
            {
                formBase.gameObject.SetActive(true);
                formBase.Open(userData);
            }

            m_OpenUIList.AddLast(formBase);
#endif

        }

        /// <summary>
        /// 检查ui是否已经打开
        /// </summary>
        /// <param name="uiformId"></param>
        /// <returns></returns>
        public bool IsExists(int uiformId)
        {
            for (LinkedListNode<UIFormBase> curr = m_OpenUIList.First; curr != null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiformId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据UIFormId关闭ui
        /// </summary>
        /// <param name="uiformId"></param>
        internal void CloseUIForm(int uiformId)
        {
            for (LinkedListNode<UIFormBase> curr = m_OpenUIList.First; curr != null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiformId)
                {
                    CloseUIForm(curr.Value);
                    break;
                }
            }
        }

        internal void CloseUIForm(UIFormBase formBase)
        {
            m_OpenUIList.Remove(formBase);
            formBase.ToClose();
        }
    }
}