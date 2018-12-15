using DbTools;
using DevExpress.Xpf.Core;
using SysCommon;
using SysCommon.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProj.View
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var conn = BaseDataBaseTools.GetDbConnection() as SqlConnection;
                var t1 = DateTime.Now;
                conn.Select("select * from grades");

                List<string> sqls = new List<string>();
                sqls.Add("select * from grades");
                sqls.Add("select * from t_sys_menu");
                var ds = conn.Select(sqls);
                tbOutput.Text += Environment.NewLine + "打开并查询成功, 耗时: " + (DateTime.Now - t1).TotalMilliseconds + "毫秒";
            }
            catch(Exception ex)
            {
                tbOutput.Text += Environment.NewLine + "异常: " + ex.Message;
            }
        }

        private void tbOutput_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbOutput.Text = "";
        }
    }
}
