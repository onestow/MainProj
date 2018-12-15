using SysCommon.Entity;
using System.Windows.Controls;

namespace SysCommon
{
    public class BasePage : Page
    {
        public Context context { get; set; }
        public BasePage() { }

        public BasePage(Context context)
        {
            this.context = context;
        }
    }
}
