using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    /// <summary>
    /// 定时器
    /// </summary>
    public class TimeAction
    {
        /// <summary>
        /// 是否运行中
        /// </summary>
        public bool IsRuning
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前运行时间
        /// </summary>
        private float m_CurrRunTime;

        /// <summary>
        /// 当前循环次数
        /// </summary>
        private int m_CurrLoop;

        /// <summary>
        /// 延迟时间
        /// </summary>
        private float m_DelayTime;

        /// <summary>
        /// 间隔 单位秒
        /// </summary>
        private float m_Interval;

        /// <summary>
        /// 循环次数
        /// -1 表示无限循环
        /// 0 也会循环一次
        /// </summary>
        private int m_Loop;

        //开始运行
        private Action m_OnStart;
        //运行中 参数表示剩余次数
        private Action<int> m_OnUpdate;
        //结束运行
        private Action m_OnComplete;

        /// <summary>
        /// 初始化
        /// </summary>
        public TimeAction Init(
            float DelayTime,
            float Interval,
            int Loop,
            Action OnStart,
            Action<int> OnUpdate,
            Action OnComplete
            )
        {
            m_DelayTime = DelayTime;
            m_Interval = Interval;
            m_Loop = Loop;
            m_OnStart = OnStart;
            m_OnUpdate = OnUpdate;
            m_OnComplete = OnComplete;
            m_CurrLoop = 0;

            return this;
        }

        public void Run()
        {
            //1.需要先把自己时间管理器的链表中
            GameEntry.Time.RegisterTimeAction(this);

            //2.设置当前运行的时间
            m_CurrRunTime = Time.time;

        }

        public void Pause()
        {
            IsRuning = false;
        }

        public void Stop()
        {
            m_OnComplete?.Invoke();

            IsRuning = false;

            //把自己从定时器链表中移除
            GameEntry.Time.RemoveTimeAction(this);

        }


        /// <summary>
        /// 每帧执行
        /// </summary>
        public void OnUpdate()
        {
            if (!IsRuning && Time.time > m_CurrRunTime + m_DelayTime)
            {
                //程序执行到这里的时候 表示已经过了第一次延迟时间
                IsRuning = true;
                m_CurrRunTime = Time.time;


                m_OnStart?.Invoke();
            }

            if (!IsRuning) return;

            if (Time.time > m_CurrRunTime)
            {
                m_CurrRunTime = Time.time + m_Interval;

                //以下代码 间隔interval时间 执行一次
                m_OnUpdate?.Invoke(m_Loop - m_CurrLoop);
              
                if (m_Loop > -1)
                {
                    m_CurrLoop++;
                    if (m_CurrLoop >= m_Loop)
                    {
                        Stop();
                    }
                }

            }
        }
    }
}