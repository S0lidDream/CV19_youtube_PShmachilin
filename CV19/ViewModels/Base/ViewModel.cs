using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Xaml;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
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

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var value_target_servise = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_servise = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            OnInitialized(value_target_servise?.TargetObject,
                          value_target_servise?.TargetProperty,
                          root_object_servise?.RootObject);

            return this;
        }

        private WeakReference _targetRef;
        private WeakReference _rootRef;

        public object TargetObject => _targetRef.Target;
        public object RootObjecet => _rootRef.Target;

        protected virtual void OnInitialized(object target, object property, object root)
        {
            _targetRef = new WeakReference(target);
            _rootRef = new WeakReference(root);
        }
    }
}
