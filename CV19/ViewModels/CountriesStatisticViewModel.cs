using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModelVM { get; }

        #region Countries : IEnumerable<CountryInfo> - Статистика по странам
        /// <summary>
        /// Статистика по странам
        /// </summary>
        private IEnumerable<CountryInfo> _Countries;
        /// <summary>
        /// Статистика по странам
        /// </summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            set => Set(ref _Countries, value);
        }
        #endregion


        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }
        #endregion


        public CountriesStatisticViewModel(MainWindowViewModel mainModelVM)
        {
            MainModelVM = mainModelVM;
            _DataService = new DataService();

            #region
            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
            #endregion

        }
    }
}
