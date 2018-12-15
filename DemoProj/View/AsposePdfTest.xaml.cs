using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Microsoft.Win32;
using SysCommon;
using SysCommon.Entity;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DemoProj.View
{
    /// <summary>
    /// AsposePdfTest.xaml 的交互逻辑
    /// </summary>
    public partial class AsposePdfTest : BasePage
    {
        public AsposePdfTest()
        {
            InitializeComponent();
        }

        public AsposePdfTest(Context ctx)
            : this()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "PDF文件|*.pdf";
            if (ofd.ShowDialog() != true)
                return;

            tbFile.Text = ofd.FileName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var filePath = tbFile.Text;
            if (!File.Exists(filePath) || Path.GetExtension(filePath).ToUpper() != ".PDF")
                return;

            var output = Path.Combine(Path.GetDirectoryName(filePath), "Output");
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            ToJpeg(filePath, output);
        }

        public void ToJpeg(string absoluteFilePath, string outputPath)
        {
            using (var pdfDocument = new Document(absoluteFilePath))
            {
                for (var i = 1; i < pdfDocument.Pages.Count + 1; i++)
                {
                    //convert a particular page and save the image to stream
                    var page = pdfDocument.Pages[i];
                    var ph = page.PageInfo.Height - page.Rect.Height;
                    var pw = page.PageInfo.Width - page.Rect.Width;
                    var pngDevice = new PngDevice((int)page.Rect.Width, (int)page.Rect.Height);
                    pngDevice.Process(page, Path.Combine(outputPath, i.ToString() + ".PNG"));
                    lblInfo.Content = $"{i}/{pdfDocument.Pages.Count}";
                }
            }
        }

        private void ExecuteLongTimeWork(Label label, string message)
        {
            var backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += (s, o) =>
            {
                System.Threading.Thread.Sleep(3000);//執行長時間工作
            };

            backgroundWorker.RunWorkerCompleted += (s, args) => {
                Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    label.Content = message;
                }));
            };
            backgroundWorker.RunWorkerAsync();
        }

    }
}
