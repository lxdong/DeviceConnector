/*-----------------------------------------------
// Copyright (C) 2019 南京戎光软件科技有限公司  版权所有。
// 文件名称：    MainWindowViewModel
// 功能描述：    MainWindowViewModel
// 创建标识：    panshuai 2019/5/16 20:37:17
// 修改标识：    panshuai 2019/5/16 20:37:17
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceConnector.Client
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            InitCommand = new DelegateCommands<string>(Init);
            InstallCommand = new DelegateCommands<string>(Install);
            UnistallCommand = new DelegateCommands<string>(Unistall);
            StartCommand = new DelegateCommands<string>(Start);
            StopCommand = new DelegateCommands<string>(Stop);
        }

        #region Comamnds
        public DelegateCommands<string> InitCommand { get; set; }
        public DelegateCommands<string> InstallCommand { get; set; }
        public DelegateCommands<string> UnistallCommand { get; set; }
        public DelegateCommands<string> StartCommand { get; set; }
        public DelegateCommands<string> StopCommand { get; set; }
        #endregion

        #region Property

        private bool? _isInit;

        public bool? IsInit
        {
            get { return _isInit; }
            set
            {
                if (_isInit != value)
                {
                    _isInit = value;
                    OnPropertyChanged("IsInit");
                }
            }
        }

        private bool? _isInstall;

        public bool? IsInstall
        {
            get { return _isInstall; }
            set
            {
                if (_isInstall!=value)
                {
                    _isInstall = value;
                    OnPropertyChanged("IsInstall");
                }
            }
        }

        private bool? _isStart;

        public bool? IsStart
        {
            get { return _isStart; }
            set
            {
                if (_isStart!=value)
                {
                    _isStart = value;
                    OnPropertyChanged("IsStart");
                }
            }
        }


        #endregion

        #region Methods


        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="param">参数</param>
        public void Init(string param = "")
        {
            ServiceSetting.Init();
        }
        #endregion

        #region 安装服务
        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="name">服务名</param>
        public void Install(string name = "")
        {
            ServiceSetting.InstallService();
        }
        #endregion

        #region 卸载服务
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void Unistall(string name = "")
        {
            ServiceSetting.UnistallService();
        }
        #endregion

        #region 启动服务
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="name"></param>
        public void Start(string name="")
        {

        }
        #endregion

        #region 停止服务
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="name"></param>
        public void Stop(string name="")
        {

        }
        #endregion

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
