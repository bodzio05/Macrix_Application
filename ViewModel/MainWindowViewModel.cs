using Macrix_Application.Data;
using Macrix_Application.Model;
using Macrix_Application.Model.Interfaces;
using Macrix_Application.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Macrix_Application.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Person> People { get; set; }

        public string XmlFilePath { get; }

        public bool AreButtonsEnabled
        {
            get 
            {
                return _areButtonsEnabled;
            }
            set
            {
                _areButtonsEnabled = value;
                OnPropertyChanged();
            }

        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        #endregion

        #region Fields
        private bool _areButtonsEnabled;
        private ObservableCollection<Person> _previousStateCollection = new ObservableCollection<Person>();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public MainWindowViewModel(string filePath)
        {
            XmlFilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            SaveCommand = new RelayCommand(param => this.Save(), param => this.CanSave());
            CancelCommand = new RelayCommand(param => this.Cancel());
            UpdateCommand = new RelayCommand(param => this.Update());


            if (People == null)
                People = new ObservableCollection<Person>();

            People.CollectionChanged += PeopleCollectionChanged;
            
            InitXmlFile();
            
            AreButtonsEnabled = false;
        }
        #endregion

        #region Private_Methods
        private void InitXmlFile()
        {
            if (File.Exists(XmlFilePath))
            {
                ReadXmlFile();
            }
        }

        private void ReadXmlFile()
        {
            var deserializedCollection = XMLSerializer.DeserializeToObject<ObservableCollection<Person>>(XmlFilePath);

            foreach (IPerson item in deserializedCollection)
            {
                People.Add(item.Clone(item));
            }

            AreButtonsEnabled = false;
        }

        private void Save()
        {
            XMLSerializer.SerializeToXml(People, XmlFilePath);

            AreButtonsEnabled = false;
        }

        private bool CanSave()
        {
            return true;
        }

        private void Cancel()
        {
            People.Clear();
            ReadXmlFile();
        }

        private void Update()
        {
            if (IsTheCollectionChanged())
            {
                AreButtonsEnabled = true;
                foreach (IPerson item in People)
                {
                    _previousStateCollection.Add(item.Clone(item));
                }
            }
            else
            {
                AreButtonsEnabled = false;
            }
        }

        private bool IsTheCollectionChanged()
        {
            var set1 = new HashSet<Person>(People);
            var set2 = new HashSet<Person>(_previousStateCollection);

            if (set1.SetEquals(set2))
                return false;
            else
                return true;
        }

        private void PeopleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Update();
        }
        #endregion

        #region Protected_Methods
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
