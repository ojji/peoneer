using System;

namespace Peoneer.Library.Utilities
{
    public class DisposableBase : IDisposable
    {
        protected bool IsDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("The object has been already disposed.");
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableBase()
        {
            Dispose(false);
        }
    }
}