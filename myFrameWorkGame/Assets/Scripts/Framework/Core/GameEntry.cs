using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class GameEntry : MonoBehaviour
    {
        #region 组件属性
        /// <summary>
        /// 事件组件
        /// </summary>
        public static EventComponent Event
        {
            get;
            private set;
        }
        /// <summary>
        /// 时间组件
        /// </summary>
        public static TimeComponent Time
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据组件
        /// </summary>
        public static DataComponent Data
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据表组件
        /// </summary>
        public static DataTableComponent DataTable
        {
            get;
            private set;
        }
        /// <summary>
        /// 下载组件
        /// </summary>
        public static DownLoadComponent Download
        {
            get;
            private set;
        }
        /// <summary>
        /// 有限状态机组件
        /// </summary>
        public static FsmComponent Fsm
        {
            get;
            private set;
        }
        public static GameObjComponent GameObj
        {
            get;
            private set;
        }
        /// <summary>
        /// Http组件
        /// </summary>
        public static HttpComponent Http
        {
            get;
            private set;
        }
        /// <summary>
        /// 本地化组件
        /// </summary>
        public static LocalizationComponent Localization
        {
            get;
            private set;
        }
        /// <summary>
        /// 对象池组件
        /// </summary>
        public static PoolComponent Pool
        {
            get;
            private set;
        }
        /// <summary>
        /// 流程化组件
        /// </summary>
        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }
        /// <summary>
        /// 资源组件
        /// </summary>
        public static ResourceComponent Resource
        {
            get;
            private set;
        }
        public static SceneComponent Scene
        {
            get;
            private set;
        }
        /// <summary>
        /// 设置组件
        /// </summary>
        public static SettingComponent Setting
        {
            get;
            private set;
        }
        /// <summary>
        /// socket组件
        /// </summary>
        public static SocketComponent Socket
        {
            get;
            private set;
        }
        /// <summary>
        /// ui组件
        /// </summary>
        public static UIComponent UI
        {
            get;
            private set;
        }
        #endregion

        #region 基础组件管理

        /// <summary>
        /// 基础组件列表
        /// </summary>
        private static readonly LinkedList<JIANINGBaseComponent> m_BaseComponentList = new LinkedList<JIANINGBaseComponent>();

        #region RegisterBaseComponent注册组件
        /// <summary>
        /// 组册组件
        /// </summary>
        /// <param name="component"></param>
        internal static void RegisterBaseComponent(JIANINGBaseComponent component)
        {
            //获取到组件类型
            Type type = component.GetType();

            LinkedListNode<JIANINGBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type)
                {
                    return;
                }

                curr = curr.Next;

            }

            //把组件加入最后一个节点
            m_BaseComponentList.AddLast(component);
        }
        #endregion

        #region GetBaseComponent获取基础组件
        /// <summary>
        /// 获取基础组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static JIANINGBaseComponent GetBaseComponent(Type type)
        {
            LinkedListNode<JIANINGBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type)
                {
                    return curr.Value;
                }

                curr = curr.Next;
            }

            return null;
        }

        public static T GetBaseComponent<T>() where T : JIANINGBaseComponent
        {
            return (T)GetBaseComponent(typeof(T));
        }
        #endregion

        #region InitBaseComponents初始化组件
        private static void InitBaseComponents()
        {
            Event = GetBaseComponent<EventComponent>();
            Time = GetBaseComponent<TimeComponent>();
            Fsm = GetBaseComponent<FsmComponent>();
            Procedure = GetBaseComponent<ProcedureComponent>();
            DataTable = GetBaseComponent<DataTableComponent>();
            Http = GetBaseComponent<HttpComponent>();
            Data = GetBaseComponent<DataComponent>();
            Localization = GetBaseComponent<LocalizationComponent>();
            Pool = GetBaseComponent<PoolComponent>();
            Scene = GetBaseComponent<SceneComponent>();
            Setting = GetBaseComponent<SettingComponent>();
            Socket = GetBaseComponent<SocketComponent>();
            UI = GetBaseComponent<UIComponent>();
            Resource = GetBaseComponent<ResourceComponent>();
            GameObj = GetBaseComponent<GameObjComponent>();
            Download = GetBaseComponent<DownLoadComponent>();
            Debug.Log("初始化组件完成");
        }
        #endregion
        #endregion

        #region 更新组件管理
        /// <summary>
        /// 基础组件列表
        /// </summary>
        private static readonly LinkedList<IUpdataComponent> m_UpdateComponentList = new LinkedList<IUpdataComponent>();

        #region RegisterUpdateComponent注册更新组件
        /// <summary>
        /// 组册更新组件
        /// </summary>
        /// <param name="component"></param>
        public static void RegisterUpdateComponent(IUpdataComponent component)
        {


            //把组件加入最后一个节点
            m_UpdateComponentList.AddLast(component);
        }
        #endregion

        #region RemoveUpdateComponent移除更新组件
        /// <summary>
        /// 移出组件
        /// </summary>
        /// <param name="component"></param>
        public static void RemoveUpdateComponent(IUpdataComponent component)
        {
            m_UpdateComponentList.Remove(component);
          
        }

        #endregion


        #endregion

        private void Awake()
        {
            InitBaseComponents();
        }


        private void Start()
        {
            
        }

        private void Update()
        {
            //循环更新组件 链表
            for (LinkedListNode<IUpdataComponent> curr = m_UpdateComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }

         
        }

        private void OnDestroy()
        {
            //关闭所有的基础组件
            for (LinkedListNode<JIANINGBaseComponent> curr = m_BaseComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.Shutdown();
            }
        }
    }
}