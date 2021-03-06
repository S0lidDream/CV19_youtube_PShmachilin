using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        /*-------------------------------------------------------------------------------------------*/
        public CountriesStatisticViewModel CountriesStatisticVM { get; }

        /*-------------------------------------------------------------------------------------------*/
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
        
        #region SelectedPageIndex : int - Номер выбранной вкладки
        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        private int _SelectedPageIndex = 1;

        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion


        #region Вкладка 0 "Разнородные данные"
        public object[] CompositeCollection { get; }

        #region SelectedCompositeValue : object - Выбранный непонятный элемент
        /// <summary>
        /// Выбранный непонятный элемент
        /// </summary>
        private object _SelectedCompositeValue;
        /// <summary>
        /// Выбранный непонятный элемент
        /// </summary>
        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }
        #endregion
        #endregion

        #region Вкладка 1 "Студенты"
        public ObservableCollection<Group> Groups { get; set; }

        #region SelectedGroup: Group - Выбранная группа
        /// <summary>
        /// Выбранная группа
        /// </summary>
        private Group _SelectedGroup;

        /// <summary>
        /// Выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if(!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion

        #region StudentFilterText : string - Текст фильтра студентов
        /// <summary>
        /// Текст фильтра студентов
        /// </summary>
        private string _StudentFilterText;

        /// <summary>
        /// Текст фильтра студентов
        /// </summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if(!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }
        #endregion

        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltred(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }
            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;
        #endregion

        #endregion

        #region Вкладка 2 "График"

        #region TestDataPoint : IEnumerable<DataPoint> - Тестовый набор данных для визууализации графиков

        private IEnumerable<DataPoint> _TestDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }

        #endregion

        #endregion

        #region Вкладка 4 "Тестирование виртуализации" 
        public IEnumerable<Student> TestStudents =>
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100000).Select(i=> new Student 
                {
                    Name = $"Имя {i}",
                    Surname = $"Фамилия {i}"
                });

        #endregion

        #region Вкладка 5 "Файловая система"
        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");

        #region SelectedDirectory : DirectoryViewModel - Выбранная директория
        /// <summary>
        /// Выбранная директория
        /// </summary>
        private DirectoryViewModel _SelectedDirectory;
        /// <summary>
        /// Выбранная директория
        /// </summary>
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }
        #endregion
        
        #endregion


        #region Status : string - Статус программы
        /// <summary>
        /// Статус программы
        /// </summary>
        private string _Status = "Готово!";
        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        /*-------------------------------------------------------------------------------------------*/
        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }
        #endregion

        #region CreateNewGroupCommand
        public ICommand CreateNewGroupCommand { get; }
        private bool CanCreateNewGroupCommandExecute(object p) => true;
        private void OnCreateNewGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion

        #region DeleteNewGroupCommand
        public ICommand DeleteNewGroupCommand { get; }
        private bool CanDeleteNewGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteNewGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
            {
                SelectedGroup = Groups[group_index];
            }
        }
        #endregion

        #endregion
        /*-------------------------------------------------------------------------------------------*/
        public MainWindowViewModel()
        {
            CountriesStatisticVM = new CountriesStatisticViewModel(this);

            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
            DeleteNewGroupCommand = new LambdaCommand(OnDeleteNewGroupCommandExecuted, CanDeleteNewGroupCommandExecute);
            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));

            for(var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });

            }

            TestDataPoints = data_points;

            int student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();
            data_list.Add("Hello world!");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();

            _SelectedGroupStudents.Filter += OnStudentFiltred;

            //Сортировка
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            //Группировка
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }


    }
}
