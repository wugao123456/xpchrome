using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPChrome
{
    /// <summary>
    /// 代理服务应可设置三种模式
    /// </summary>
    public enum ProxyType:int
    {
        /// <summary>
        /// 本地：可读取Dicom图生成WADO接口
        /// </summary>
        LOCAL = 0,
        /// <summary>
        /// 云PACS：转发Request
        /// </summary>
        CloudPACS = 1,
        /// <summary>
        /// AI平台：
        /// </summary>
        AIPLATFORM = 2
    }



}
