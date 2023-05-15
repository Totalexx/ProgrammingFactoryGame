using System;
using System.Reflection;

namespace RoslynCSharp
{
    public class AppDomainProxy : MarshalByRefObject, IDisposable
    {

        public void LoadAssembly(byte[] assembly)
        {
            var asm = Assembly.Load(assembly);
        }
        
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            return;
        }
    }
}