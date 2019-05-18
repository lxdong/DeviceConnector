/*-----------------------------------------------
// Copyright (C) 2019 南京戎光软件科技有限公司  版权所有。
// 文件名称：    DelegateCommands
// 功能描述：    
// 创建标识：    panshuai 2019/5/16 20:38:09
// 修改标识：    panshuai 2019/5/16 20:38:09
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeviceConnector.Client
{
    public class DelegateCommands<T> : ICommand
    {
        private readonly Action<T> _executeMethod = null;
        private readonly Func<T, bool> _canExecuteMethod = null;

        public DelegateCommands(Action<T> executeMethod)
            : this(executeMethod, null)
        { }

        public DelegateCommands(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMetnod");
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        #region ICommand 成员
        /// <summary>
        ///  Method to determine if the command can be executed
        /// </summary>
        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
            }
            return true;

        }

        /// <summary>
        ///  Execution of the command
        /// </summary>
        public void Execute(T parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod(parameter);
            }
        }

        #endregion


        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region ICommand 成员

        public bool CanExecute(object parameter)
        {
            if (parameter == null && typeof(T).IsValueType)
            {
                return (_canExecuteMethod == null);

            }

            return CanExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion
    }
}
