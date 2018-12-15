using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SysCommon
{
    public class ReflectionTools
    {
        #region 单例
        private static ReflectionTools instance;
        private static object _lock = new object();
        private ReflectionTools() { }

        public static ReflectionTools Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(_lock)
                    {
                        if (instance == null)
                            instance = new ReflectionTools();
                    }
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, Assembly> dictLoadedAssembly = new Dictionary<string, Assembly>();
        public Assembly LoadDll(string assemblyPath)
        {
            var fullPath = Path.GetFullPath(assemblyPath).ToLower();
            if (!dictLoadedAssembly.ContainsKey(fullPath))
            {
                lock(dictLoadedAssembly)
                {
                    if (!dictLoadedAssembly.ContainsKey(fullPath))
                    {
                        var assembly = Assembly.LoadFile(fullPath);
                        if(assembly == null)
                            throw new DllNotFoundException($"未能加载程序集 ({assemblyPath}), 完整路径: {fullPath}。请确认文件是否存在或文件格式是否正确");
                        dictLoadedAssembly.Add(fullPath, Assembly.LoadFile(fullPath));
                    }
                }
            }

            return dictLoadedAssembly[fullPath];
        }

        public Type LoadType(string assemblyPath, string className)
        {
            var assembly = LoadDll(assemblyPath);
            var type = assembly.GetType(className);
            if (type == null)
                throw new EntryPointNotFoundException($"在程序集({assemblyPath})中找不到类({className})");

            return type;
        }

        public T CreateInstance<T>(string assemblyPath, string className, object[] args)
        {
            var type = LoadType(assemblyPath, className);
            var obj = Activator.CreateInstance(type, args);
            return (T)obj;
        }
    }
}
