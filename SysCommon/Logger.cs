using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SysCommon
{
    public class Logger
    {

        private static ILog log = LogManager.GetLogger(typeof(Logger));
        public static void Init()
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            if (!File.Exists(configPath))
                throw new Exception("找不到日志配置文件");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
       }

        public static void Error(string msg)
        {
            log.Error("Application Load failed : " + msg);
        }

        public static void Info(string msg)
        {
            log.Info(msg);
        }

        public static void Debug(string msg)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Application loaded successfully." + msg);
            }
        }

        public static void Warn(string msg)
        {

            log.Warn(msg);
        }
    }
}
