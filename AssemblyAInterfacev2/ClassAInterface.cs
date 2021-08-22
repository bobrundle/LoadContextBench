using System;

namespace AssemblyAInterface
{
    public interface IClassAv1
    {
        public string OriginalMethod();
    }
    public interface IClassAv2
    {
        public string OriginalMethod();
        public string NewMethod();
    }
}
