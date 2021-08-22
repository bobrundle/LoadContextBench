using AssemblyAInterface;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace LoadContextBench
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our versioned assemblts are in separate directories

            string apathv1 = Path.Combine(Directory.GetCurrentDirectory(), @"..\v1\net5.0\AssemblyA.dll");
            string apathv2 = Path.Combine(Directory.GetCurrentDirectory(), @"..\v2\net5.0\AssemblyA.dll");

            // Separate load contexts for the different versions.

            AssemblyLoadContext alcv1 = new AssemblyLoadContext("v1");
            AssemblyLoadContext alcv2 = new AssemblyLoadContext("v2");
//            AssemblyLoadContext alcv1 = new MyLoadContext(apathv1);
//            AssemblyLoadContext alcv2 = new MyLoadContext(apathv2);

            // Load v1 and v2 of the same assembly

            Assembly av1 = alcv1.LoadFromAssemblyPath(apathv1);
            Assembly av2 = alcv2.LoadFromAssemblyPath(apathv2);

            // Look for our types in the various contexts.  Doesn't exist in default context.

            Type t0 = Type.GetType("NamespaceA.ClassA"); // null in default context.
            Type tv1 = av1.GetType("NamespaceA.ClassA");
            Type tv2 = av2.GetType("NamespaceA.ClassA");

            // Create v1 and v2 objects.  Reference interfaces.  Call methods.

            try
            {
                var dv1 = Activator.CreateInstance(tv1);
                var dv2 = Activator.CreateInstance(tv2);
                IClassAv1 cv1 = (IClassAv1)dv1;
                IClassAv1 cv1a = (IClassAv1)dv2;
                IClassAv2 cv2 = (IClassAv2)dv2; // v2 only
                Console.WriteLine(cv1.OriginalMethod());
                Console.WriteLine(cv1a.OriginalMethod());
                Console.WriteLine(cv2.NewMethod()); // v2 only 
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
