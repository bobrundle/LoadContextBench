using AssemblyAInterface;
using System;

namespace NamespaceA
{
    public class ClassA : IClassAv1, IClassAv2
    {
        public string OriginalMethod() { return "this is the original method in v2"; }
        public string NewMethod() { return "this is a new method in v2"; }
    }
}
