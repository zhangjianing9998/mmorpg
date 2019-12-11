﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Fsm<T> : FsmBase where T : class
    {

        /// <summary>
        /// 当前状态
        /// </summary>
        private FsmState<T> m_CurrState;


        /// <summary>
        /// 状态字典
        /// </summary>
        private Dictionary<byte, FsmState<T>> m_StateDic;

        /// <summary>
        /// 参数字典
        /// </summary>
        private Dictionary<string, VariableBase> m_ParamDic;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fsmId">状态机编号</param>
        /// <param name="Owner">拥有者</param>
        /// <param name="states">状态数组</param>
        public Fsm(int fsmId, T Owner, FsmState<T>[] states) : base(fsmId)
        {
            m_StateDic = new Dictionary<byte, FsmState<T>>();
            m_ParamDic = new Dictionary<string, VariableBase>();
            //把状态加入字典
            int len = states.Length;
            for (int i = 0; i < len; i++)
            {
                FsmState<T> state = states[i];
                state.CurrFsm = this;
                m_StateDic[(byte)i] = state;
            }

            //设置默认状态
            CurrStateType = 0;

            m_CurrState = m_StateDic[CurrStateType];
            m_CurrState.OnEnter();
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public FsmState<T> GetState(byte stateType)
        {
            FsmState<T> state = null;
            m_StateDic.TryGetValue(stateType, out state);

            return state;
        }

        public void OnUpdate()
        {
            if (m_CurrState != null)
            {
                m_CurrState.OnUpdate();
            }
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(byte newState)
        {
            if (newState == CurrStateType)
            {
                Debug.LogError("Fsm状态重复");
                return;
            }
            if (m_CurrState != null)
            {
                m_CurrState.OnLeave();
            }

            CurrStateType = newState;

            m_CurrState = m_StateDic[CurrStateType];

            //进入新状态
            m_CurrState.OnEnter();

        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <typeparam name="TData">泛型的类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetDate<TData>(string key, TData value)
        {
            VariableBase itemBase = null;
            if (m_ParamDic.TryGetValue(key, out itemBase))
            {
                Variable<TData> item = itemBase as Variable<TData>;
                item.Value = value;
                m_ParamDic[key] = item;

            }
            else
            {
                //参数原来不存在
                Variable<TData> item = new Variable<TData>();
                item.Value = value;
                m_ParamDic[key] = item;
            }
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TData GetData<TData>(string key)
        {
            VariableBase itemBase = null;
            if (m_ParamDic.TryGetValue(key, out itemBase))
            {
                Variable<TData> item = itemBase as Variable<TData>;
                return item.Value;
            }
            else
            {
                return default(TData);
            }
        }

        /// <summary>
        /// 关闭状态机
        /// </summary>
        public override void Shutdown()
        {
            if (m_CurrState != null)
            {
                m_CurrState.OnLeave();
            }
            foreach (KeyValuePair<byte, FsmState<T>> state in m_StateDic)
            {
                state.Value.OnDestory();
            }
            m_StateDic.Clear();
            m_ParamDic.Clear();
        }
    }
}