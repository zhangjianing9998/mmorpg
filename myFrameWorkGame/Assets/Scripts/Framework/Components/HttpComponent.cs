using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class HttpComponent : JIANINGBaseComponent
    {
        [SerializeField]
        [Header("正式账号服务器Url")]
        private string m_WebAccountuUrl = "";

        [SerializeField]
        [Header("测试账号服务器Url")]
        private string m_TestWebAccountuUrl = "";

        [SerializeField]
        [Header("是否测试环境")]
        private bool m_IsTest;

        /// <summary>
        /// 真实账号服务器Url
        /// </summary>
        public string RealWebAccountUrl
        {
            get
            {
                return m_IsTest ? m_TestWebAccountuUrl : m_WebAccountuUrl;
            }
        }

        private HttpManager m_HttpManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_HttpManager = new HttpManager();
        }


        /// <summary>
        /// 发送web数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="isPost"></param>
        /// <param name="isGetData">是否获取字节数据</param>
        /// <param name="dic"></param>
        public void SendData(string url, HttpSendDataCallBack callBack, bool isPost = false, bool isGetData = false, Dictionary<string, object> dic = null)
        {
            m_HttpManager.SendData(url,callBack,isPost,isGetData,dic);
        }



        public override void Shutdown()
        {

        }
    }
}
