using SysCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProj.BaseClass
{
    public class RelayCommand<T> : ICommand
		where T : class
    {
		private Predicate<T> canExecute = null;
		private Action<T> executeAction = null;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayCommand(Action<T> executeAction, Predicate<T> canExecute = null)
		{
			this.executeAction = executeAction ?? throw new ArgumentException("实现方法不能为空");
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute?.Invoke(parameter as T) ?? true;
		}

		public void Execute(object parameter)
		{
			executeAction(parameter as T);
		}
    }
}
