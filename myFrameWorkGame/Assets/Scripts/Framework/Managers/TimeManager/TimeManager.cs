using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class TimeManager : ManagerBase, System.IDisposable
    {
        /// <summary>
        /// 定时器链表
        /// </summary>
        private LinkedList<TimeAction> m_TimeAction;

        public TimeManager()
        {
            m_TimeAction = new LinkedList<TimeAction>();
        }

        /// <summary>
        /// 注册定时器
        /// </summary>
        /// <param name="action"></param>
        internal void RegisterTimeAction(TimeAction action)
        {

            m_TimeAction.AddLast(action);


        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        /// <param name="action"></param>
        internal void RemoveTimeAction(TimeAction action)
        {
            m_TimeAction.Remove(action);
        }

        internal void OnUpdate()
        {
            for (
                LinkedListNode<TimeAction> curr = m_TimeAction.First
                ; curr != null
                ; curr = curr.Next
                )
            {
                curr.Value.OnUpdate();

            }
        }

        public void Dispose()
        {
            m_TimeAction.Clear();
        }
    }
}
