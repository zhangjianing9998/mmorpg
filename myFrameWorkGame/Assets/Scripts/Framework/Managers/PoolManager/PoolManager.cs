using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class PoolManager : ManagerBase, System.IDisposable
    {
        public ClassObjectPool ClassObjectPool
        {
            get;
            private set;
        }

        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
        }

        /// <summary>
        /// 释放类对象池
        /// </summary>
        public void ClearClassObjectPool()
        {
            ClassObjectPool.Clear();

        }


        public void Dispose()
        {
            ClassObjectPool.Dispose();
        }
    }
}
