using System;
using System.Collections.Generic;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок главного окна
        private string _Title = "Анализ статистики CV19";

        /// <summary>
        /// Заголовок главного окна
        /// </summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    if (Equals(_Title, value)) return;
            //    _Title = value;
            //    OnPropertyChanged();

            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }
        #endregion


    }
}
