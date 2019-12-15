﻿using System;
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

            InitGameObjectPool();
            
        }

        protected override void OnStart()
        {
            base.OnStart();
            InitClassSetReside();
        }

        /// <summary>
        /// 初始化常用类常驻数量
        /// </summary>
        private void InitClassSetReside()
        {
            GameEntry.Pool.SetClassObjectResideCount<HttpRoutine>(3);
            GameEntry.Pool.SetClassObjectResideCount<Dictionary<string,object>>(3);
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


        #region 变量对象池

        /// <summary>
        /// 变量对象池锁
        /// </summary>
        private object m_VarObjectLock = new object();

#if UNITY_EDITOR
        /// <summary>
        /// 监视器面板变量池信息
        /// </summary>
        public Dictionary<Type, int> VarObjectInspectorDic = new Dictionary<Type, int>();
#endif

        /// <summary>
        /// 取出一个变量对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueVarObject<T>() where T : VariableBase, new()
        {
            lock (m_VarObjectLock)
            {
                T item = DequeueClassObject<T>();
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]++;
                }
                else
                {
                    VarObjectInspectorDic[t] = 1;
                }
#endif

                return item;
            }


        }
        /// <summary>
        /// 变量对象回池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void EnqueueVarObject<T>(T item) where T : VariableBase
        {
            lock (m_VarObjectLock)
            {
                EnqueueClassObject(item);
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]--;
                    if (VarObjectInspectorDic[t] == 0)
                    {
                        VarObjectInspectorDic.Remove(t);
                    }
                }

#endif


            }
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


        #region 游戏物体对象池

        /// <summary>
        /// 对象池的分组
        /// </summary>
        [SerializeField]
        private GameObjectPoolEntity[] m_GameObjectPoolGroups;

        /// <summary>
        /// 初始化游戏物体对象池
        /// </summary>
        public void InitGameObjectPool()
        {
            StartCoroutine(PoolManager.GameObjectPool.Init(m_GameObjectPoolGroups, transform));
        }

        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="prefab"></param>
        /// <param name="onComplete"></param>
        public void GameObjectSpawn(byte poolId, Transform prefab, System.Action<Transform> onComplete)
        {
            PoolManager.GameObjectPool.Spawn(poolId, prefab, onComplete);
        }

        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="instance"></param>
        public void GameObjectDespawn(byte poolId, Transform instance)
        {
            PoolManager.GameObjectPool.Despawn(poolId, instance);
        }
        #endregion
    }
}