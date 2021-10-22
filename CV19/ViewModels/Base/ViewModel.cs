using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Реализация интерфейса IDisposable
        private bool _Disposed;

        //~ViewModel()
        //{
        //    Dispose(false);
        //}
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(!disposing || _Disposed)
            {
                return;
            }
            else
            {
                _Disposed = true;
            }
            //Освобождение управляемых ресурсов
        }
        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }
    }
}
