using SysCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProj.Model
{
    public class MenuModel : BaseModel
    {
        public string Icon { get; set; }
        public string MenuName { get; set; }
        public string assemblyPath { get; set; }
        public string className { get; set; }
        public List<MenuModel> SubMenu { get; set; }

        public MenuModel()
        {
            SubMenu = new List<MenuModel>();
        }
    }
}
