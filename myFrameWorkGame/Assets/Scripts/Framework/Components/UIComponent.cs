using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JIANING
{
    public class UIComponent : JIANINGBaseComponent, IUpdataComponent
    {
        [SerializeField]
        [Header("标准分辨率宽度")]
        private int m_StandarWidth = 1280;

        [SerializeField]
        [Header("标准分辨率高度")]
        private int m_StandarHeigth = 720;

        [SerializeField]
        [Header("UI摄像机")]
        public Camera UICamera;

        [SerializeField]
        [Header("根画布")]
        private Canvas m_UIRootCanvas;

        [SerializeField]
        [Header("根画布的缩放")]
        private CanvasScaler m_UIRootScaler;

        [SerializeField]
        [Header("UI分组")]
        private UIGroup[] UIGroups;


        private UIManager m_UIManager;

        private UILayer m_UILayer;

        private UIPool m_UIPool;

        [Header("释放间隔（秒）")]
        [SerializeField]
        private float m_ClearInterval = 120f;

        /// <summary>
        /// UI回池后过期时间
        /// </summary>
        public float UIExpire = 120f;

        /// <summary>
        /// 下次运行时间
        /// </summary>
        private float m_NextRunTime = 0f;

        /// <summary>
        /// UI对象池中最大的数量
        /// </summary>
        public int UIPoolMaxCount = 5;

        private Dictionary<byte, UIGroup> m_UIGroupDic;


        /// <summary>
        /// 标准分辨率比值
        /// </summary>
        private float m_StandarScreen = 0;

        /// <summary>
        /// 当前分辨率比值
        /// </summary>
        private float m_CurrScreen = 0;



        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_UIGroupDic = new Dictionary<byte, UIGroup>();
            m_StandarScreen = m_StandarWidth / (float)m_StandarHeigth;
            m_CurrScreen = Screen.width / (float)Screen.height;

            NormalFormCanvasScaler();


            int len = UIGroups.Length;
            for (int i = 0; i < len; i++)
            {
                UIGroup group = UIGroups[i];
                m_UIGroupDic[group.Id] = group;
            }

            m_UIManager = new UIManager();
            m_UILayer = new UILayer();
            m_UIPool = new UIPool();


            m_UILayer.Init(UIGroups);
        }

        #region UI适配
        /// <summary>
        /// LoadingForm自动适配
        /// </summary>
        public void LoadingFormCanvasScaler()
        {
            if (m_CurrScreen > m_StandarScreen)
            {
                //设置为0
                m_UIRootScaler.matchWidthOrHeight = 0;
            }
            else
            {
                m_UIRootScaler.matchWidthOrHeight = m_StandarScreen - m_CurrScreen;
            }
        }

        /// <summary>
        /// 全屏窗口FullForm适配缩放
        /// </summary>
        public void FullFormCanvasScaler()
        {
            m_UIRootScaler.matchWidthOrHeight = 1;
        }

        /// <summary>
        /// 普通Form自动适配
        /// </summary>
        public void NormalFormCanvasScaler()
        {
            m_UIRootScaler.matchWidthOrHeight = (m_CurrScreen >= m_StandarScreen) ? 1 : 0;

        }
        #endregion

        #region GetUIGroup 根据UI分组编号获取UI分组
        /// <summary>
        /// 根据UI分组编号获取UI分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UIGroup GetUIGroup(byte id)
        {
            UIGroup group = null;
            m_UIGroupDic.TryGetValue(id, out group);
            return group;
        }
        #endregion

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <param name="userData"></param>
        public void OpenUIForm(int uiFormId, object userData = null)
        {
            m_UIPool.CheckByOpenUI();
            m_UIManager.OpenUIForm(uiFormId, userData);
        }

        /// <summary>
        /// 关闭UI窗体
        /// </summary>
        /// <param name="formBase"></param>
        public void CloseUIForm(UIFormBase formBase)
        {
            m_UIManager.CloseUIForm(formBase);
        }

        /// <summary>
        /// 根据UIFormId关闭ui
        /// </summary>
        /// <param name="uiformId"></param>
        public void CloseUIForm(int uiformId)
        {
            m_UIManager.CloseUIForm(uiformId);
        }

        /// <summary>
        /// 设置层级排序
        /// </summary>
        /// <param name="formBase"></param>
        /// <param name="isAdd"></param>
        public void SetSortingOrder(UIFormBase formBase, bool isAdd)
        {
            m_UILayer.SetSortingOrder(formBase, isAdd);
        }

        /// <summary>
        /// 从UI对象池中获取ui
        /// </summary>
        /// <param name="uiformId"></param>
        /// <returns></returns>
        public UIFormBase Dequeue(int uiformId)
        {
            return m_UIPool.Dequeue(uiformId);
        }

        /// <summary>
        /// ui回池
        /// </summary>
        public void Enqueue(UIFormBase uiformBase)
        {
            m_UIPool.Enqueue(uiformBase);
        }

        public void OnUpdate()
        {
            if (Time.time > m_NextRunTime + m_ClearInterval)
            {
                m_NextRunTime = Time.time;

                //释放UI对象池
                m_UIPool.CheckClear();
            }
        }

        public override void Shutdown()
        {

        }
    }
}