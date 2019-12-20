using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class HttpManager : ManagerBase
    {



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
            Debug.Log("从池中获取http访问器");

            HttpRoutine http = GameEntry.Pool.DequeueClassObject<HttpRoutine>();
            http.SendData(url,callBack,isPost, isGetData, dic);

        }
        

    }
}
