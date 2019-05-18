/*-----------------------------------------------
// Copyright (C) 2019 南京戎光软件科技有限公司  版权所有。
// 文件名称：    ServiceSetting
// 功能描述：    服务设置类
// 创建标识：    panshuai 2019/5/16 20:28:48
// 修改标识：    panshuai 2019/5/16 20:28:48
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceConnector.Client
{
    public static class ServiceSetting
    {

        public static void Init()
        {
            string directory = Path.Combine(System.Environment.CurrentDirectory, "Service");
            string name = "DeviceConnector.Service.exe";
            string service = "DeviceConnector";
            string path = Path.Combine(directory, name);
            string Installbat_content = @"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe {0}
Net Start {1}
sc config {1} start = auto";
            File.WriteAllText(Path.Combine(directory, "Install.bat"), string.Format(Installbat_content, path, service));

            string Unistallbat_content = @"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe /u {0}";
            File.WriteAllText(Path.Combine(directory, "Uninstall.bat"), string.Format(Unistallbat_content, path));
        }

        public static bool InstallService()
        {
            bool result = false;
            try
            {
                string directory = Path.Combine(System.Environment.CurrentDirectory, "Service");
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = directory+"\\Install.bat";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                //System.Environment.CurrentDirectory = directory;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static bool UnistallService()
        {
            bool result = false;
            try
            {
                string directory = Path.Combine(System.Environment.CurrentDirectory, "Service");
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = directory+ "\\Uninstall.bat";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                result = true;
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}
