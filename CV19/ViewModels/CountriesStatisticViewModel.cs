using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private readonly DataService _DataService;
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

        #region SelectedCountry : CountryInfo - Выбранная страна
        /// <summary>
        /// Выбранная страна
        /// </summary>
        private CountryInfo _SelectedCountry;

        /// <summary>
        /// Выбранная страна
        /// </summary>
        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
        }
        #endregion



        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }
        #endregion

        /// <summary>
        /// Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере
        /// </summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Вызов конструктора, предназначенного для отладки");

            _Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {i}",
                ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Province {i}",
                    Location = new System.Windows.Point(i, j),
                    Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                    {
                        Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                        Count = k
                    }).ToArray()
                }).ToArray()
            }).ToArray();
        }


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
