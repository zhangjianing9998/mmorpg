using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class UIPool
    {
        /// <summary>
        /// ������е��б�
        /// </summary>
        private LinkedList<UIFormBase> m_UIFormList;


        public UIPool()
        {
            m_UIFormList = new LinkedList<UIFormBase>();
        }

        /// <summary>
        /// ��UI������л�ȡui
        /// </summary>
        /// <param name="uiformId"></param>
        /// <returns></returns>
        internal UIFormBase Dequeue(int uiformId)
        {
            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiformId)
                {
                    m_UIFormList.Remove(curr.Value);
                    return curr.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// ui�س�
        /// </summary>
        internal void Enqueue(UIFormBase uiformBase)
        {
            uiformBase.gameObject.SetActive(false);
            m_UIFormList.AddLast(uiformBase);
        }

        /// <summary>
        /// ����Ƿ�����ͷ�
        /// </summary>
        internal void CheckClear()
        {
            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {
                if (!curr.Value.IsLock && Time.time > (curr.Value.CloseTime + GameEntry.UI.UIExpire))
                {
                    //����UI
                    Object.Destroy(curr.Value.gameObject);


                    LinkedListNode<UIFormBase> next = curr.Next;
                    m_UIFormList.Remove(curr.Value);
                    curr = next;
                }
                else
                {
                    curr = curr.Next;
                }

            }
        }


        /// <summary>
        /// ��uiʱ����Ƿ��п��ͷŵ�
        /// </summary>
        internal void CheckByOpenUI()
        {
            if (m_UIFormList.Count <= GameEntry.UI.UIPoolMaxCount)
            {
                return;
            }

            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {

                ///������е�������ָ������������ ���ټ�������
                if (m_UIFormList.Count == GameEntry.UI.UIPoolMaxCount + 1)
                {
                    break;
                }

                if (!curr.Value.IsLock)
                {
                    //����UI
                    Object.Destroy(curr.Value.gameObject);


                    LinkedListNode<UIFormBase> next = curr.Next;
                    m_UIFormList.Remove(curr.Value);
                    curr = next;
                }
                else
                {
                    curr = curr.Next;
                }

            }
        }

    }
}