using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    public class UIPool
    {
        /// <summary>
        /// 对象池中的列表
        /// </summary>
        private LinkedList<UIFormBase> m_UIFormList;


        public UIPool()
        {
            m_UIFormList = new LinkedList<UIFormBase>();
        }

        /// <summary>
        /// 从UI对象池中获取ui
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
        /// ui回池
        /// </summary>
        internal void Enqueue(UIFormBase uiformBase)
        {
            uiformBase.gameObject.SetActive(false);
            m_UIFormList.AddLast(uiformBase);
        }

        /// <summary>
        /// 检查是否可以释放
        /// </summary>
        internal void CheckClear()
        {
            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {
                if (!curr.Value.IsLock && Time.time > (curr.Value.CloseTime + GameEntry.UI.UIExpire))
                {
                    //销毁UI
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
        /// 打开ui时检查是否有可释放的
        /// </summary>
        internal void CheckByOpenUI()
        {
            if (m_UIFormList.Count <= GameEntry.UI.UIPoolMaxCount)
            {
                return;
            }

            for (LinkedListNode<UIFormBase> curr = m_UIFormList.First; curr != null;)
            {

                ///如果池中的数量在指定的数量以内 则不再继续销毁
                if (m_UIFormList.Count == GameEntry.UI.UIPoolMaxCount + 1)
                {
                    break;
                }

                if (!curr.Value.IsLock)
                {
                    //销毁UI
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