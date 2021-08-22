using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace LoadContextBench
{
    public class MyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;
        public MyLoadContext(string assemblyPath)
        {
            _resolver = new AssemblyDependencyResolver(assemblyPath);
        }
        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }
    }
}
