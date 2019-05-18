/*-----------------------------------------------
// Copyright (C) 2019 南京戎光软件科技有限公司  版权所有。
// 文件名称：    IMessageBus
// 功能描述：    
// 创建标识：    panshuai 2019/5/17 10:39:08
// 修改标识：    panshuai 2019/5/17 10:39:08
// 修改描述:     
-----------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceConnector.Core
{
    public interface IMessageBus
    {
        void Init();
        void Connect();
        void Disconnect();
        void Receive();
        void Send();
    }
}
