using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class PoolComponent : JIANINGBaseComponent, IUpdataComponent
    {
        public PoolManager PoolManager
        {
            get;
            private set;
        }



        protected override void OnAwake()
        {
            base.OnAwake();
            PoolManager = new PoolManager();
            GameEntry.RegisterUpdateComponent(this);
            m_NextRunTime = Time.time;
        }

        #region Dequeue 取出一个对象
        /// <summary>
        /// 取出一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueClassObject<T>() where T : class, new()
        {
            return PoolManager.ClassObjectPool.DequeueClassObject<T>();
        }

        #endregion

        #region Enqueue 对象回池
        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="obj"></param>
        public void EnqueueClassObject(object obj)
        {

            PoolManager.ClassObjectPool.EnqueueClassObject(obj);

        }
        #endregion

        #region SetClassObjectResideCount 设置类对象池常驻数量
        /// <summary>
        /// 设置类对象池常驻数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetClassObjectResideCount<T>(byte count) where T : class
        {
            PoolManager.ClassObjectPool.SetResideCount<T>(count);
        }
        #endregion


        public override void Shutdown()
        {
            PoolManager.Dispose();

        }

        /// <summary>
        /// 释放间隔
        /// </summary>
        [SerializeField]
        public int m_ClearInterval;

        /// <summary>
        /// 下次运行时间
        /// </summary>
        private float m_NextRunTime = 0f;

        public void OnUpdate()
        {
            if (Time.time > m_NextRunTime + m_ClearInterval)
            {
                //该释放了
                m_NextRunTime = Time.time;

                //释放类对象池
                PoolManager.ClearClassObjectPool();

            }
        }



    }
}