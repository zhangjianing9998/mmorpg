using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    /// <summary>
	/// 更新组件接口
	/// </summary>
	public interface IUpdataComponent 
	{
        /// <summary>
        /// 更新方法
        /// </summary>
		void OnUpdate();

        /// <summary>
        /// 实例的编号
        /// </summary>
        int InstanceID { get; }
	}
}