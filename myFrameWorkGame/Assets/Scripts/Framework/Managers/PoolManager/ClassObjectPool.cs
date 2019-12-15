using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    /// <summary>
    /// 类对象池
    /// </summary>
    public class ClassObjectPool : System.IDisposable
    {

        /// <summary>
        /// 类对象池常驻数量
        /// </summary>
        public Dictionary<int, byte> ClassObjectCount
        {
            get;
            private set;
        }



        private Dictionary<int, Queue<object>> m_ClassObjectPoolDic;

#if UNITY_EDITOR
        /// <summary>
        /// 监视器面板显示的信息
        /// </summary>
        public Dictionary<Type, int> InspectorDic = new Dictionary<Type, int>();
#endif

        public ClassObjectPool()
        {
            ClassObjectCount = new Dictionary<int, byte>();
            m_ClassObjectPoolDic = new Dictionary<int, Queue<object>>();
        }


        #region SetResideCount 设置类对象池常驻数量
        /// <summary>
        /// 设置类对象池常驻数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetResideCount<T>(byte count) where T : class
        {
            int key = typeof(T).GetHashCode();
            ClassObjectCount[key] = count;
        }
        #endregion

        #region DequeueClassObject 取出一个对象
        /// <summary>
        /// 取出一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueClassObject<T>() where T : class, new()
        {
            lock (m_ClassObjectPoolDic)
            {
                //先找到这个类的hash码
                int key = typeof(T).GetHashCode();

                Queue<object> queue = null;
                m_ClassObjectPoolDic.TryGetValue(key, out queue);

                if (queue == null)
                {
                    queue = new Queue<object>();
                    m_ClassObjectPoolDic[key] = queue;
                }

                //开始获取对象
                if (queue.Count > 0)
                {
                    //说明队列中有闲置的对象
                    Debug.Log("对象存在 从池中获取");
                    object obj = queue.Dequeue();
#if UNITY_EDITOR
                    Type t = obj.GetType();
                    if (InspectorDic.ContainsKey(t))
                    {
                        InspectorDic[t]--;
                    }
                    else
                    {
                        InspectorDic[t] = 0;
                    }
#endif
                    return (T)obj;
                }
                else
                {
                    //说明队列中没有闲置的
                    Debug.Log("对象不存在实例化" + key);
                    return new T();
                }

            }

        }


        #endregion

        #region EnqueueClassObject 对象回池
        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="obj"></param>
        public void EnqueueClassObject(object obj)
        {
            lock (m_ClassObjectPoolDic)
            {
                int key = obj.GetType().GetHashCode();
                Debug.Log("对象回池" + key);
                Queue<object> queue = null;
                m_ClassObjectPoolDic.TryGetValue(key, out queue);

#if UNITY_EDITOR
                Type t = obj.GetType();
                if (InspectorDic.ContainsKey(t))
                {
                    InspectorDic[t]++;
                }
                else
                {
                    InspectorDic[t] = 1;
                }
#endif
                if (queue != null)
                {
                    queue.Enqueue(obj);
                }
            }
        }
        #endregion


        /// <summary>
        /// 释放类对象池
        /// </summary>
        public void Clear()
        {
            lock (m_ClassObjectPoolDic)
            {

                Debug.Log("释放类对象池");

                int queueCount = 0;//队列的数量
                //1.定义迭代器
                var enumerator = m_ClassObjectPoolDic.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    int key = enumerator.Current.Key;

                    Queue<object> queue = m_ClassObjectPoolDic[key];

#if UNITY_EDITOR
                    Type t = null;
#endif
                    queueCount = queue.Count;

                    //用于释放的时候 判断常驻数量
                    byte resideCount = 0;
                    ClassObjectCount.TryGetValue(key, out resideCount);
                    while (queueCount > resideCount)
                    {
                        queueCount--;
                        //说明队列中有可释放的对象
                        object obj = queue.Dequeue();//从队列中取出一个 这个对象没有任何引用，就变成了野指针 那他就等待GC回收
#if UNITY_EDITOR
                        t = obj.GetType();
                        InspectorDic[t]--;
#endif
                    }

                    if (queueCount == 0)
                    {
#if UNITY_EDITOR
                        if (t != null)
                        {
                            InspectorDic.Remove(t);
                        }
#endif
                    }

                }

                //GC 整个项目中，有一处GC即可
                GC.Collect();

            }
        }

        public void Dispose()
        {
            m_ClassObjectPoolDic.Clear();
        }


    }
}