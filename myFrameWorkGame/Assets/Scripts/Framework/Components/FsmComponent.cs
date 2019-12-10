using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    /// <summary>
    /// 状态机的组件
    /// </summary>
    public class FsmComponent : JIANINGBaseComponent
    {
        /// <summary>
        /// 状态机管理器
        /// </summary>
        public FsmManager m_FsmManager;

        /// <summary>
        /// 状态机临时编号
        /// </summary>
        private int m_TempId = 0;

        protected override void OnAwake()
        {
            base.OnAwake();

            m_FsmManager = new FsmManager();
        }

        #region Create 创建状态机
        /// <summary>
        /// 创建状态机
        /// </summary>
        /// <typeparam name="T">拥有者的类型</typeparam>
        /// <param name="fsmId">状态机编号</param>
        /// <param name="owner">拥有者</param>
        /// <param name="states">状态数组</param>
        /// <returns></returns>
        public Fsm<T> Create<T>( T owner, FsmState<T>[] states) where T : class
        {
            return m_FsmManager.Create<T>(m_TempId++,owner,states);
        }
        #endregion

        #region DestoryFsm 销毁状态机
        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="fsmId"></param>
        public void DestoryFsm(int fsmId)
        {
            m_FsmManager.DestoryFsm(fsmId);

        }
        #endregion

        public override void Shutdown()
        {

            m_FsmManager.Dispose();
        }
    }
}