using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModelVM { get; }
        public CountriesStatisticViewModel(MainWindowViewModel mainModelVM)
        {
            MainModelVM = mainModelVM;
            _DataService = new DataService();
        }
    }
}
