using SysCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MainProj
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "Main";
            Logger.Init();
            //Logger.Error("Hello Error");
            //Logger.Debug("Hello Debug");
            //Logger.Info("Hello Info");
        }
    }
}
