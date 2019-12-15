using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace JIANING
{

    /// <summary>
    /// http发送数据的回调委托
    /// </summary>
    /// <param name="args"></param>
    public delegate void HttpSendDataCallBack(HttpCallbackArgs args);
    /// <summary>
    /// http访问器
    /// </summary>
    public class HttpRoutine
    {
        #region 属性

        /// <summary>
        /// Http请求回调
        /// </summary>
        private HttpSendDataCallBack m_CallBack;

        /// <summary>
        /// Http请求回调数据
        /// </summary>
        private HttpCallbackArgs m_CallBackArgs;

        /// <summary>
        /// 是否繁忙
        /// </summary>
        public bool IsBusy
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否获取data数据
        /// </summary>
        private bool m_IsGetData = false;
        #endregion

        public HttpRoutine()
        {
            m_CallBackArgs = new HttpCallbackArgs();
        }

        #region SendData 发送web数据
        /// <summary>
        /// 发送web数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="isPost"></param>
        /// <param name="isGetData">是否获取字节数据</param>
        /// <param name="dic"></param>
        public void SendData(string url,
            HttpSendDataCallBack callBack,
            bool isPost = false,
            bool isGetData = false,
            Dictionary<string, object> dic = null)
        {
            if (IsBusy) return;

            IsBusy = true;
            m_CallBack = callBack;
            m_IsGetData = isGetData;

            if (!isPost)
            {
                GetUrl(url);
            }
            else
            {
                //web加密
                if (dic != null)
                {
                    //客户端标识符
                    dic["deviceIdentifier"] = DeviceUtil.DeviceIdentifier;

                    //设备型号
                    dic["deviceModel"] = DeviceUtil.DeviceModel;

                    long t = GameEntry.Data.SysDataManager.CurrServerTime;
                    //签名
                    dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", t, DeviceUtil.DeviceIdentifier));

                    //时间戳
                    dic["t"] = t;
                }

                string json = string.Empty;
                if (dic != null)
                {
                    json = JsonMapper.ToJson(dic);
                    if (!m_IsGetData)
                    {
                        GameEntry.Log(LogCategory.Proto, "<color=#ffa200>发送消息:</color><color=#FFFB80>" + url + "</color>");
                        GameEntry.Log(LogCategory.Proto, "<color=#ffdeb3>==>>" + json + "</color>");
                    }
                    GameEntry.Pool.EnqueueClassObject(dic);
                }

                PostUrl(url, json);
            }
        }
        #endregion

        #region GetUrl Get请求
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        private void GetUrl(string url)
        {

            UnityWebRequest data = UnityWebRequest.Get(url);
            GameEntry.Http.StartCoroutine(Request(data));
        }
        #endregion

        #region PostUrl Post请求
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        private void PostUrl(string url, string json)
        {
            //定义一个表单
            WWWForm form = new WWWForm();

            //给表单添加值
            form.AddField("", json);

            UnityWebRequest data = UnityWebRequest.Post(url, form);
            GameEntry.Http.StartCoroutine(Request(data));
        }
        #endregion

        #region Request 请求服务器
        /// <summary>
        /// 请求服务器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator Request(UnityWebRequest data)
        {
            yield return data.SendWebRequest();

            IsBusy = false;
            if (data.isNetworkError || data.isHttpError)
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = true;
                    m_CallBackArgs.Value = data.error;

                    if (!m_IsGetData)
                    {
                        GameEntry.Log(LogCategory.Proto, "<color=#00eaff>接收消息:</color><color=#00ff9c>" + data.url + "</color>");
                        GameEntry.Log(LogCategory.Proto, "<color=#c5e1dc>==>>" + JsonUtility.ToJson(m_CallBackArgs) + "</color>");
                    }
                    m_CallBack(m_CallBackArgs);
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = false;
                    m_CallBackArgs.Value = data.downloadHandler.text;

                    if (!m_IsGetData)
                    {
                        GameEntry.Log(LogCategory.Proto, "<color=#00eaff>接收消息:</color><color=#00ff9c>" + data.url + "</color>");
                        GameEntry.Log(LogCategory.Proto, "<color=#c5e1dc>==>>" + JsonUtility.ToJson(m_CallBackArgs) + "</color>");
                    }
                    m_CallBackArgs.Data = data.downloadHandler.data;
                    m_CallBack(m_CallBackArgs);
                }
            }
            data.Dispose();
            data = null;

            //Debug.Log("把http访问器回池");
            GameEntry.Pool.EnqueueClassObject(this);
        }
        #endregion


    }
}