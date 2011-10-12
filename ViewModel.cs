using System;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using ODataCrudWP7.PeopleService;

namespace ODataCrudWP7
{
    public class ViewModel : INotifyPropertyChanged
    {
        // Fields...
        private DataServiceCollection<Person> _People;
        private Person _SelectedPerson;
        private readonly Database1Entities context;

        /// <summary>
        /// Initializes a new instance of the ViewModel class.
        /// </summary>
        public ViewModel()
        {
            context = new Database1Entities(new Uri("http://localhost:12312/PeopleService.svc", UriKind.Absolute));
            People = new DataServiceCollection<Person>(context);
            LoadData();
        }

        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                if (_SelectedPerson == value)
                    return;
                _SelectedPerson = value;
                RaisePropertyChanged("Person");
            }
        }

        public DataServiceCollection<Person> People
        {
            get { return _People; }
            set
            {
                if (_People == value)
                    return;
                _People = value;
                RaisePropertyChanged("People");
            }
        }

        public void LoadData()
        {
            SelectedPerson = null;
            People = new DataServiceCollection<Person>(context);
            People.LoadAsync(context.People);
        }

        public void ClearData()
        {
            People.Clear();
        }

        public void DeleteCurrentItem()
        {
            context.DeleteObject(SelectedPerson);
            context.BeginSaveChanges((cb) =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var response = context.EndSaveChanges(cb);
                    LoadData();
                });
            }, null);
        }

        public void SaveCurrentItem()
        {
            if (SelectedPerson.PersonID == 0)
            {
                context.AddToPeople(SelectedPerson);
            }
            else
            {
                context.UpdateObject(SelectedPerson);
            }
            context.BeginSaveChanges((cb) =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var response = context.EndSaveChanges(cb);
                    LoadData();
                });
            }, null);
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INPC
    }
}