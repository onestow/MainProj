using DbTools;
using SysCommon;
using SysCommon.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DemoProj.View
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class DataBaseTest : BasePage
    {
        public DataBaseTest(Context context)
            : base(context)
        {
            InitializeComponent();
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            tbOutput.Dispatcher.Invoke(() =>
            {
                tbOutput.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
            });
        }
        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var threadCount = int.Parse(tbThreadCount.Text);
            ThreadPool.SetMaxThreads(threadCount, threadCount);
            
            for (int i = 0; i < 1000; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoAction), i);
            }
        }

        public void DoAction(object obj)
        {
            try
            {
                using (var conn = BaseDataBaseTools.GetDbConnection() as SqlConnection)
                {
                    var t1 = DateTime.Now;
                    conn.Select("select * from grades");

                    List<string> sqls = new List<string>();
                    sqls.Add("select * from grades");
                    sqls.Add("select * from t_sys_menu");
                    var ds = conn.Select(sqls);
                    Logger.Info(obj.ToString() + "打开并查询成功, 耗时: " + (DateTime.Now - t1).TotalMilliseconds + "毫秒");
                }
            }
            catch (Exception ex)
            {
                Logger.Info(obj.ToString() + "===========" + ex.Message);
            }
        }

        private void tbOutput_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbOutput.Text = "";
        }
    }
}
