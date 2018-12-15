using SysCommon;
using SysCommon.Entity;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DemoProj.View
{
    /// <summary>
    /// FileWatcherTest.xaml 的交互逻辑
    /// </summary>
    public partial class FileWatcherTest : BasePage
    {
        FileSystemWatcher watcher;
        public FileWatcherTest(Context context)
            : base(context)
        {
            InitializeComponent();
            tbFilePath.Text = @"D:\temp";
        }

        private void StartDirectoryListen(string path)
        {
            if(watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }

            tb.Text = "";
            if(!Directory.Exists(path))
            {
                tb.Text = "文件夹不存在";
                return;
            }

            tb.Text = "开始监听文件夹: " + path;
            watcher = new FileSystemWatcher();
            watcher.BeginInit();
            //watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = 0
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName
                | NotifyFilters.Attributes
                | NotifyFilters.Size
                | NotifyFilters.LastWrite
                | NotifyFilters.LastAccess
                | NotifyFilters.CreationTime
                | NotifyFilters.Security
                ;
            watcher.Path = path;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
            watcher.EndInit();
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            output(Environment.NewLine + "重命名了文件或目录: " + e.OldFullPath + "->" + e.FullPath + "  " + e.ChangeType);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            output(Environment.NewLine + "删除了文件或目录: " + e.FullPath + "  " + e.ChangeType);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            output(Environment.NewLine + "修改了文件或目录: " + e.FullPath + "  " + e.ChangeType);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            output(Environment.NewLine + "创建了文件或目录: " + e.FullPath + "  " + e.ChangeType);
        }

        private void output(string msg)
        {
            tb.Dispatcher.Invoke(() =>
             {
                 tb.Text += msg;
             });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartDirectoryListen(tbFilePath.Text);
        }
    }
}
