using System;

namespace JIANING
{
	/// <summary>
	/// Http请求的回调数据
	/// </summary>
	public class HttpCallbackArgs : EventArgs
	{
		/// <summary>
		/// 是否有错
		/// </summary>
		public bool HasError;

		/// <summary>
		/// 返回值
		/// </summary>
		public string Value;

		/// <summary>
		/// 字节数据
		/// </summary>
		public byte[] Data;
	}
}