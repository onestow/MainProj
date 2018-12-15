using MainProj.Model;
using SysCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProj.ViewModel
{
    class MenuViewModel : BaseViewModel
    {
        public MenuModel Menu { get; set; }
        public MenuViewModel()
        {
            Menu = new MenuModel();
        }
    }
}
