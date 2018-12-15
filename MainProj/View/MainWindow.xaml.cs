using DbTools;
using DevExpress.Xpf.Core.Internal;
using MainProj.Model;
using SysCommon;
using SysCommon.Entity;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MainProj.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Context context;
        public MainWindow()
        {
            InitializeComponent();
            InitNavi();
            context = new Context
            {
                userCode = "000",
                userName = "tom",
                password = "000"
            };
        }

        #region 初始化导航菜单
        private void InitNavi()
        {
            var dtMenu = DbHelper.Query("select * from t_sys_menu");
            List<MenuModel> menus = new List<MenuModel>();

            FillMenuModel(menus, "0000000000", dtMenu);

            tvNavi.ItemsSource = menus;
        }

        private void FillMenuModel(List<MenuModel> menus, string parentMenuCode, DataTable dtMenu)
        {
            var drsMenu = dtMenu.Select($"s_parentcode = '{parentMenuCode}'");
            if (drsMenu == null || drsMenu.Count() < 1)
                return;

            foreach(var drMenu in drsMenu)
            {
                var model = new MenuModel()
                {
                    Icon = null,
                    MenuName = drMenu["s_menuname"].ToString(),
                    assemblyPath = drMenu["s_assembly"].ToString(),
                    className = drMenu["s_class"].ToString()
                };
                menus.Add(model);

                FillMenuModel(menus.Last().SubMenu, drMenu["s_menucode"].ToString(), dtMenu);
            }
        }

        //private void InitNavi2()
        //{
        //    var dtMenu = DbHelper.Query("select * from t_sys_menu");
        //    var drsTop = dtMenu.Select("s_parentcode = '0000000000'");

        //    foreach(var drTop in drsTop)
        //    {
        //        Label lblHeader = new Label();
        //        lblHeader.Content = drTop["s_menuname"].ToString();

        //        var sPanel = new StackPanel();
        //        var drsSub = dtMenu.Select($"s_parentcode = '{drTop["s_menucode"]}'");
        //        foreach(var drSub in drsSub)
        //        {
        //            var lblSub = new Label();
        //            lblSub.Content = drSub["s_menuname"].ToString();
        //            sPanel.Children.Add(lblSub);
        //        }

        //        var epd = new Expander();
        //        epd.ExpandDirection = ExpandDirection.Down;
        //        epd.Header = lblHeader;
        //        epd.Content = sPanel;

        //        LeftNavi.Children.Add(epd);
        //    }
        //}
        #endregion

        private bool ShowWindow(string name, string assemblyPath, string className)
        {
            if (string.IsNullOrEmpty(assemblyPath) || string.IsNullOrEmpty(className))
                return false;

            TabItem selectedTabItem = null;

            foreach(TabItem showedTi in SubWindowsArea.Items)
            {
                if((showedTi.Content as Frame).Content?.GetType().FullName == className)
                {
                    selectedTabItem = showedTi;
                    break;
                }
            }

            if (selectedTabItem == null)
            {
                var typePage = ReflectionTools.Instance.LoadType(assemblyPath, className);

                var f = new Frame();
                f.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                f.Content = Activator.CreateInstance(typePage, new object[] { context });

                selectedTabItem = new TabItem();
                selectedTabItem.Header = name;
                selectedTabItem.Content = f;

                SubWindowsArea.Items.Add(selectedTabItem);
            }

            selectedTabItem.IsSelected = true;
            return true;
        }

        private void tvNavi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = tvNavi.SelectedItem as MenuModel;
            if (string.IsNullOrWhiteSpace(item.assemblyPath))
                return;
            ShowWindow(item.MenuName, item.assemblyPath, item.className);
        }
    }
}
