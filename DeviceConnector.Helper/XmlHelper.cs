/*-----------------------------------------------
// Copyright (C) 2018 南京戎光软件科技有限公司  版权所有。
// 文件名称：    XmlHelper
// 功能描述：    
// 创建标识：    panshuai 2018-10-30 8:44:31
// 修改标识：    panshuai 2018-10-30 8:44:31
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeviceConnector.Helper
{
    public class XmlHelper
    {
        private List<DeviceModel> _devices;

        public List<DeviceModel> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }

        private List<DescribeModel> _describes;

        public List<DescribeModel> Describes
        {
            get { return _describes; }
            set { _describes = value; }
        }


        public XmlHelper()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Files\\SystemData.xml";
            InitData(filePath);
        }

        public XmlHelper(string filePath)
        {
            InitData(filePath);
        }

        private void InitData(string filePath)
        {
            _describes = new List<DescribeModel>();
            _devices = new List<DeviceModel>();
            if (System.IO.File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList desNodes = doc.SelectSingleNode("BAS").SelectSingleNode("Describes").ChildNodes;
                XmlNodeList devNodes = doc.SelectSingleNode("BAS").SelectSingleNode("Devices").ChildNodes;
                foreach (XmlNode node in desNodes)
                {
                    XmlElement element = (XmlElement)node;
                    DescribeModel model = new DescribeModel()
                    {
                        Code = element.GetAttribute("Code"),
                        Value = element.GetAttribute("Value"),
                        Remark = element.GetAttribute("Remark")
                    };
                    _describes.Add(model);
                }
                foreach (XmlNode node in devNodes)
                {
                    XmlElement element = (XmlElement)node;
                    DeviceModel model = new DeviceModel()
                    {
                        Code = element.GetAttribute("Code"),
                        Value = element.GetAttribute("Value"),
                        Floor = element.GetAttribute("Floor")
                    };
                    _devices.Add(model);
                }
            }

        }

        public string GetFullName(string device, string tag)
        {
            string result = "";
            var query = _devices.Where(t => t.Code == device);
            if (query.Any())
            {
                string group = query.First().Value;
                result = string.Format("{0}.{1}.{2}", group, device, tag);
            }
            return result;
        }

        public DescribeModel GetDescribe(string tag)
        {
            DescribeModel model = new DescribeModel();
            var query = _describes.Where(t => tag.Contains(t.Code));
            if (query.Any())
            {
                model = query.First();
            }
            return model;
        }

        public string GetFloor(string device)
        {
            string result = "";
            var query = _devices.Where(t => t.Code == device);
            if (query.Any())
            {
                result = query.First().Floor;
            }
            return result;
        }

    }

    public class DeviceModel
    {
        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _floor;

        public string Floor
        {
            get { return _floor; }
            set { _floor = value; }
        }


    }

    public class DescribeModel
    {
        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _group;

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }

        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

    }

}
